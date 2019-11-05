namespace NovaData

open FSharp.Data

module Proxy =
    [<Literal>] //can safely make design time connection any DB
    let DesignTimeConn = @"Data Source=.;Initial Catalog=NeoProxy;Integrated Security=True"

    type TableExists = SqlCommandProvider<"
    DECLARE @stagingTableName AS NVARCHAR(100) = @proxyTableName
    DECLARE @dataTableName AS NVARCHAR(100) = @proxyTableName

    SELECT
    	CASE WHEN (SELECT 1 
               FROM INFORMATION_SCHEMA.TABLES 
               WHERE TABLE_TYPE = 'BASE TABLE' 
    		   AND TABLE_SCHEMA = 'Staging'
               AND TABLE_NAME = @stagingTableName) IS NULL THEN 0 ELSE 1 END AS InStaging
    	,CASE WHEN (SELECT 1 
               FROM INFORMATION_SCHEMA.TABLES 
               WHERE TABLE_TYPE = 'BASE TABLE' 
    		   AND TABLE_SCHEMA = 'Data'
               AND TABLE_NAME = @dataTableName) IS NULL THEN 0 ELSE 1 END AS InData

    ", DesignTimeConn, SingleRow = true>

    type RunCmd = SqlCommandProvider<"
    DECLARE @myCmd AS NVARCHAR(max) = @Command

    DECLARE @cmd nvarchar(max)
    DECLARE cmds CURSOR FOR
    SELECT @myCmd

    OPEN cmds
    WHILE 1 = 1
    BEGIN
        FETCH cmds INTO @cmd
        IF @@fetch_status != 0 BREAK
        EXEC(@cmd)
    END
    CLOSE cmds;
    DEALLOCATE cmds
    ", DesignTimeConn>


    type ProxyColumnMetaFromProxy = SqlCommandProvider<"
    SELECT COLUMN_NAME
        ,ORDINAL_POSITION
        ,IS_NULLABLE
        ,DATA_TYPE
        ,CHARACTER_MAXIMUM_LENGTH
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_SCHEMA = @TableSchema
      AND TABLE_NAME = @TableName
    ORDER BY ORDINAL_POSITION
        ", DesignTimeConn>

    type ProxyColumnDDL = SqlCommandProvider<"
    SELECT TABLE_SCHEMA 
        ,TABLE_NAME
        ,COLUMN_NAME
        ,ORDINAL_POSITION
        ,IS_NULLABLE
        ,DATA_TYPE
        ,CHARACTER_MAXIMUM_LENGTH
    FROM INFORMATION_SCHEMA.COLUMNS
    ORDER BY TABLE_SCHEMA 
        ,TABLE_NAME
        ,ORDINAL_POSITION
        ", DesignTimeConn>

    let getProxyColumnMetaFromProxy schema name conn =
         use cmd = new ProxyColumnMetaFromProxy(connectionString = conn)
         cmd.Execute(TableSchema = schema, TableName = name)
         |> Seq.toArray

    type ProxyKeyMetaFromProxy = SqlCommandProvider<"
    SELECT COLUMN_NAME
        ,ORDINAL_POSITION
    FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
    WHERE TABLE_SCHEMA = @TableSchema
      AND TABLE_NAME = @TableName
    ORDER BY ORDINAL_POSITION
        ", DesignTimeConn>

    type ProxyKeyDDL = SqlCommandProvider<"
    SELECT TABLE_SCHEMA 
        ,TABLE_NAME
        ,COLUMN_NAME
        ,ORDINAL_POSITION
    FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
    ORDER BY TABLE_SCHEMA 
        ,TABLE_NAME
        ,ORDINAL_POSITION
        ", DesignTimeConn>

    let getProxyKeyMetaFromProxy schema name conn =
        use cmd = new ProxyKeyMetaFromProxy(connectionString = conn)
        cmd.Execute(TableSchema = schema, TableName = name)
        |> Seq.toArray

    let getColumnLookup conn =
        use cmd = new ProxyColumnDDL(connectionString = conn)
        cmd.Execute()
        |> Seq.toArray

    let proxyTables conn =
        let columnLookup = getColumnLookup conn

        use cmd = new ProxyKeyDDL(connectionString = conn)
        cmd.Execute()
        |> Seq.toArray
