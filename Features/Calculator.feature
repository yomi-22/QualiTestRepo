Feature: Shopping List
As a customer, I would like to add 
item to basket, manage the items 
in the basket and view the prices


Scenario: User should able to manage their shopping list and view the final price
	Given I add four random items to my cart
	When I view my cart
	Then I find total four items listed in my cart
	When I search for lowest price item
	And I am able to remove the lowest price item from my cart
	Then I am able to verify three items in my cart