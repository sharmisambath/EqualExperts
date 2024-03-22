# Shopping Cart 

Shopping Cart is a C# application that provides shopping cart capabilities such as adding a product to a cart and obtain the state of the cart (such as cart items, tax payable, cart subtotal, total cost etc)

## Assumptions

The following assumptions were made before developing the solution 
1. Product Details are available to the end user
2. The product title is treated as the identity parameter and is assumed to be unique and constant.
3. Only mentioned capabilities in the problem solution has been considered for developing the solution
4. The problem statement mentioned that no app, such as web APIs, browser, desktop, or command-line applications has to be submitted as the solution. Hence only the business logic layer and data access layer has been coded. (Extension for the controller layer can be achieved if needed)

## Solution Overview 
1. The solution is divided into two components - ShoppingCart and ShoppingCartTests
2. The ShoppingCart component contains the various functionalities for achieving the capabilities of the shopping cart. We have used .NET 6 to develop the application
3. The ShoppingCartTest component contains Unit Test Cases which are developed using xUnit and Moq

## Service Layer
1. The service layer contains the ShoppingCartService.
2. The functionalities provided by this service include,
   - Get prodcuts in the cart
   - Add a product to a cart
   - Obtain the cart state (products in cart + quantity, cart subtotal, tac payable and total)
   - Remove product from a cart (reduces prodcut quantity by 1)
   - Clear entire cart
  
## Repository Layer 
1. The product price is fetched from the given API using from the product repository service. (Ideally the repository layer is used for connecting to the data source to retrieve information, here the API is considered as the data source for fetching information relevant to a product)

## Unit Tests
1. The unit test cases for the solution are separated as a seperate solution.
2. The unit test cases are aimed at covering the business logic or service layer and the repository layer.

#End

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
## :warning: Please read these instructions carefully and entirely first
* Clone this repository to your local machine.
* Use your IDE of choice to complete the assignment.
* When you have completed the assignment, you need to  push your code to this repository and [mark the assignment as completed by clicking here](https://app.snapcode.review/submission_links/6ddda116-e9af-4e9e-804c-9ebeac52a53e).
* Once you mark it as completed, your access to this repository will be revoked. Please make sure that you have completed the assignment and pushed all code from your local machine to this repository before you click the link.
* There is no time limit for this task - however, for guidance, it is expected to typically take around 1-2 hours.
    
# Begin the task

Write some code that provides the following basic shopping cart capabilities:

1. Add a product to the cart
   1. Specifying the product name and quantity
   2. Retrieve the product price by issuing a request to the the [Price API](#price-api) specified below
   3. Cart state (totals, etc.) must be available

2. Calculate the state:
   1. Cart subtotal (sum of price for all items)
   2. Tax payable (charged at 12.5% on the subtotal)
   3. Total payable (subtotal + tax)
   4. Totals should be rounded up where required

## Price API

The price API is an HTTP service that returns the price details for a product, identified by it's name. The shopping cart should integrate with the price API to retrieve product prices. 

### Price API Service Details

Base URL: `https://equalexperts.github.io/`

View Product: `GET /backend-take-home-test-data/{product}.json`

List of available products
* `cheerios`
* `cornflakes`
* `frosties`
* `shreddies`
* `weetabix`

## Example
The below is a sample with the correct values you can use to confirm your calculations

### Inputs
* Add 1 × cornflakes @ 2.52 each
* Add another 1 x cornflakes @2.52 each
* Add 1 × weetabix @ 9.98 each
  
### Results  
* Cart contains 2 x cornflakes
* Cart contains 1 x weetabix
* Subtotal = 15.02
* Tax = 1.88
* Total = 16.90

## Tips on what we’re looking for

We value simplicity as an architectural virtue and as a development practice. Solutions should reflect the difficulty of the assigned task, and shouldn’t be overly complex. We prefer simple, well tested solutions over clever solutions. 

### DO

* ✅ Include unit tests.
* ✅ Test both any client and logic.
* ✅ Update the README.md with any relevant information, assumptions, and/or tradeoffs you would like to highlight.

### DO NOT

* ❌ Submit any form of app, such as web APIs, browser, desktop, or command-line applications.
* ❌ Add unnecessary layers of abstraction.
* ❌ Add unnecessary patterns/ architectural features that aren’t called for e.g. persistent storage.


