@web @mercuryWebsite
Feature: Join Mercury

As a potential customer
I want to be able to join Mercury
So that I can enjoy Mercury's offerings

Scenario: New customer signs up for Electricity - Standard User - 1 year fixed term
	Given I elect to sign up for Electricity only
	And I enter a valid supply address
	And I have selected usage type 'Standard User'
	And I have chosen the '1 Year fixed' offer
	And I have entered valid personal details
	And I have entered valid driver licence details
	And I have entered the following property details:
	| Preferred Start Date | Current Address | Vulnerable person | Medical Equipment | Dog |
	| ASAP                 | Yes             | No                | No                | No  |
	And I have consented to credit checks
	And I have accepted the Terms and Conditions
	When I click to sign up
	Then I should be signed up successfully