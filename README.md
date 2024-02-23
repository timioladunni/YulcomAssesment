# YulcomAssesment
Hi Team, This is my assessment solution for Yulcoms
Please kindly note the following items to be able to run the code locally.

1) The Seeded Database is in the DataAccessLibrary class Library where the DB Migrations will be run.
 In the connection string, the path is already  pointing to the bin file in your local path
e.g connection string 
"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={your local path}\\YulcomAssesment\\DataAccessLibrary\\YulcomDatabase.mdf;Integrated Security=True"

2)The app-settings configuration is encrypted with a TripleDESCryptoServiceProvider Algorithm for security concerns so to pass your local path, If you want to change your connection string, you either have to encrypt your connection string with that Encryption, or remove the Decryption Utility in the Program.cs file 


3)The CRUD operations endpoints require a JWT token for it to be accessed which can be  gotten from the generatetoken endpoint passing the default client id and client secret values which are in your postman collection

4)The application default data is seeded once the application is run and the application also utilizes both Interface and Implementation service and the mediator Function for data manipulation and some of the features of the dotnet 8 framework

5)There’s also a Postman collection that has the value the request and test cases for easier integration and documentation to understand the endpoints
