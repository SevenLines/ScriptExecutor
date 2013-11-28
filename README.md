ScriptExecutor
==============
Simple command line sql select script executor for MSSQL

  -c, --command      Required. sql script or path to file with sql script

  -l, --delimiter    (Default: \t) columns delimeter

  -s, --source       (Default: (local)) database server

  -d, --database     Required. DataBase name

  -u, --user         user

  -p, --password     password

  -v, --verbose      verbose

  -o, --output       output file path

  -e, --encoding     (Default: 1251) Code page, see Encoding .Net class doc for
                     the list of available values

  --help             Display help screen.
  
  Examples:
  
  1. Output result to output.txt

  ScriptExecutor.exe -c "SELECT * FROM TableName WHERE id in (1,2,3)" -d "SimpleDataBase" >output.txt
  
  2. Load script from file
  
  ScriptExecutor.exe -c "script.sql" -d "SimpleDataBase" >output.txt

  3. Load script from file and ouput result to file using UTF8 encoding

  ScriptExecutor.exe -c "script.sql" -d "SimpleDataBase" -o "output.txt" -v -e "65001"
