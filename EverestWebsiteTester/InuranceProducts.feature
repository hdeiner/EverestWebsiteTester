Feature: Insurance Products 

Scenario Outline: Insurance Products Popup
	Given I navigate to the Everest Website
	And I examine the insurance menu
	When I select the insurance products
	Then I should see the insurance product called "<productName>" on line "<lineNumber>"
	
	Examples:
	| productName       | lineNumber |
	| Property          | 1          |
	| Casuality         | 2          |
	| Financial Lines   | 3          |
	| Speciality        | 4          |
	| Accident & Health | 5          |
	| Programs          | 6          |
