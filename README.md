Hello,

I have generated backup of development DB and added in files., so whenever testing happens Back needs to be restored and web.config needs to be changed with its respective connection striing.

Following are the APIs Generated for the Assignment.
Category APIs :
http://localhost:51825/api/ShopBridgeAPI/GetAllCategories?companyId=1
http://localhost:51825/api/ShopBridgeAPI/GetCategoryByID?ID=1&companyId=1

Product APIs : 
http://localhost:51825/api/ShopBridgeAPI/GetAllProducts?companyId=1
http://localhost:51825/api/ShopBridgeAPI/GetAllProductsByCategoryID?companyId=1&CategoryID=1
http://localhost:51825/api/ShopBridgeAPI/GetProductByID?ID=2&companyId=1
