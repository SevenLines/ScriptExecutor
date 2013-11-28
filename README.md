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
