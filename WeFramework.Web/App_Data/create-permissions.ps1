$Connection = New-Object System.Data.SQLClient.SQLConnection

[System.Reflection.Assembly]::LoadFrom((get-item $PSScriptRoot ).parent.FullName+'\bin\WeFramework.Web.dll') | Out-null

Add-Type -AssemblyName System.Configuration
[appdomain]::CurrentDomain.SetData("APP_CONFIG_FILE", (get-item $PSScriptRoot ).parent.FullName+'\Web.config')
[Configuration.ConfigurationManager].GetField("s_initState", "NonPublic, Static").SetValue($null, 0)
[Configuration.ConfigurationManager].GetField("s_configSystem", "NonPublic, Static").SetValue($null, $null)
([Configuration.ConfigurationManager].Assembly.GetTypes() | where {$_.FullName -eq "System.Configuration.ClientConfigPaths"})[0].GetField("s_current", "NonPublic, Static").SetValue($null, $null)
$Connection.ConnectionString=[System.Configuration.ConfigurationManager]::ConnectionStrings["firstConnectionString"].ConnectionString

$MyPermissionProvider = new-object WeFramework.Web.Infrastructure.PermissionProvider

$MyPermissions=$MyPermissionProvider.GetPermissions()

$Connection.Open()

#查询现存的系统权限表

$Command = New-Object System.Data.SQLClient.SQLCommand
$Command.Connection = $Connection
$Command.CommandText = "SELECT * FROM Permission"
$MyDataSet = New-Object System.Data.DataSet
$MyAdapter = New-Object System.Data.SqlClient.SqlDataAdapter($Command)
[void]$MyAdapter.Fill($MyDataSet)

#向数据库添加新增的权限列表

foreach($permission in $MyPermissions) 
{
    $DatabaseRecordExists = $false

    foreach($row in $MyDataSet.Tables[0].Rows)
    {
      if($permission.Name -eq $row["Name"])
      {
         $DatabaseRecordExists = $true
      }
    }
    if($DatabaseRecordExists -eq $false)
    {
         $sql ="INSERT INTO [Permission](Category,Name,Description) VALUES (@Category,@Name,@Description)"  
         $Command = New-Object System.Data.SQLClient.SQLCommand
         $Command.Connection = $Connection
         $Command.CommandText = $sql
         $Command.Parameters.AddWithValue("@Category", $permission.Category) | Out-Null
         $Command.Parameters.AddWithValue("@Name", $permission.Name) | Out-Null
		 $Command.Parameters.AddWithValue("@Description", $permission.Description) | Out-Null
         $Command.ExecuteNonQuery() | Out-Null
         
         $permission
    }
	else
	{
		 $sql ="UPDATE [Permission] SET Category=@Category,Name=@Name,Description=@Description WHERE Name=@Name"  
         $Command = New-Object System.Data.SQLClient.SQLCommand
         $Command.Connection = $Connection
         $Command.CommandText = $sql
         $Command.Parameters.AddWithValue("@Category", $permission.Category) | Out-Null
         $Command.Parameters.AddWithValue("@Name", $permission.Name) | Out-Null
		 $Command.Parameters.AddWithValue("@Description", $permission.Description) | Out-Null
         $Command.ExecuteNonQuery() | Out-Null
         
         $permission
	}
}

#从数据库删除无效的权限列表

foreach($row in $MyDataSet.Tables[0].Rows)
{
    $CodeRecordExists = $false

    foreach($permission in $MyPermissions)
    {
      if($permission.Name -eq $row["Name"])
      {
         $CodeRecordExists = $true
      }
    }
    if($CodeRecordExists -eq $false)
    {
         $sql ="DELETE FROM RolePermission WHERE PermissionID=@PermissionID"  
         $Command = New-Object System.Data.SQLClient.SQLCommand
         $Command.Connection = $Connection
         $Command.CommandText = $sql
         $Command.Parameters.AddWithValue("@PermissionID", $row["ID"]) | Out-Null
         $Command.ExecuteNonQuery() | Out-Null

         $sql ="DELETE FROM Permission WHERE ID=@ID"  
         $Command = New-Object System.Data.SQLClient.SQLCommand
         $Command.Connection = $Connection
         $Command.CommandText = $sql
         $Command.Parameters.AddWithValue("@ID", $row["ID"]) | Out-Null
         $Command.ExecuteNonQuery() | Out-Null
    }
}

$Connection.Close()

exit


#powershell –NonInteractive –ExecutionPolicy Unrestricted create-permissions.ps1