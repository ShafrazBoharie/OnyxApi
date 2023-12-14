# OnyxApi

* APIs are secured using AuthO
* tests\Onyx.Postman has the the postman collection with the following endpoints
  * Endpoint to get a JWT token
  * Health endpoint
  * Get all products
  * Get products by colour
 
* Product data are created in the in-memory database with an Entity framework

 # TODO:
  The following tasks should be carried out if I spend more time.
  * Add rate limiting guard
  * Include client validation to validate the correct entry for colour when filtering products by colour.
  * Configure Swagger to incorporate Authorize capability so that the token can be generated from the Swagger document.
