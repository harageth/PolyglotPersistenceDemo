This application is meant as a simple cloud file storage system similar to Dropbox, Google Drive, or Box.
It is not meant in any way to reflect a subversioning system(although I have a feeling it could be added with a third database that is a column/family database such as Hbase or Cassandra)

The reason for the development of this app was to show a use case where a document database would be better than a relational model so that I could give a guest lecture
to EWU's advanced database class. It just so happened that I absolutely loved building this. 

I may add ER diagrams comparing what my schema looks like to what it would need to look like in a relational model at some point... but not at this exact moment.

Required Technologies to get this running:
Mongodb - Mongodb is very easy to install and get running. When I get some time I will write my own quick tutorial but in the meantime follow this link and then find 
the OS that you are using http://docs.mongodb.org/manual/installation/
After getting Mongo installed you will need to be running a local instance of mongod (the database server) for this application to actually work.
Redis   - Head to this link and download the windows binaries http://redis.io/download. After Redis is downloaded you will need to find a proper file location to place it.
When placed in the correct file location run redis-server from the proper system files (32 or 64 bit depending on what system you are running)

.NET Framework 4.5 - I built the project completely in .NET 4.5 for ease to me. This was not meant as a full fledged application but more of a 
proof of concept/demonstration application/rapid prototype of what can be done and this seemed the best way to me.
Visual Studio 2012 (recommended) -  built the system in vs2012 because that is what I have and wanted everything to be in one project in one application. (This may be required for my solution files but I am not positive)

After you have the servers running and the project open you will need to make sure that the database is initialized. 
In PolyglotDemo.Data.Test there is a DatabaseInitialize class with a static method called DatabaseInitializeFactory. This method can either be passed the strings "Mongo" or "Redis"
to initialize that database with data. I would recommend removing the commented out lines from Page_Load in Default.aspx.cs that would initialize the database.
After removing the comments run the application once. Close the application and recomment those lines out. After those lines are now commented out you may run the application again
and it should work fine as the databases should be initialized on your local systems now.



Known Issues:
Linkbuttons for files that are not in the base directory return the wrong value to their eventhandler.
Create Folder has a hard coded folder name that it is adding to the CWD in the file structure.
Loging into the system (there will only ever be two users able to login to this system. Myself and a guest. Guests will have a TTL on their data of 30 min)
Error checking (currently there aren't any try/catches I think anywhere in any of my solutions. So please play nice for now.)
Limit on file upload size based on IIS server configurations (there is a n HTTP limit as well since all of this is being done through that protocal.. but again this is a demo app and not meant for production)