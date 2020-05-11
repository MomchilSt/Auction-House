# Auction-House


## Roles
First registration will acquire administrator functionality.
Every other after has user functionality.

## Functionality
Only administrator can delete and cities to which they can assign auction house and then create/delete auction for that auction house.
Users can bid for auction or buyout directly when auction's time ends or bought directly user generates a receipt with auction's info.

## Set up Cloudinary (required)
1. Register a [Cloudinary](https://cloudinary.com/) account.
2. [Create a Cloud, API key and API secret]
2. In the *Web/Auction.Web/appsettings.json* configuration file insert the Cloud name, API key and API secret.

## Set up Db
Change SQL connection string located in *Web/Auction.Web/appsettings.json*
