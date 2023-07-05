@web @myAccount
Feature: Login

As a registered My Account user
I want to be able to log in
So that I can manage my account

Scenario: Registered My Account user attempts to log in
    Given I am on the login page
    And I am a registered My Account user
    When I log in with valid credentials
    Then I should be logged in

Scenario: Login to My Account without any credentials
	Given I am on the login page
	When they attempt to log in without entering any credentials
	Then they should see errors for the missing email and password

Scenario: Login to My Account with an email address but no password
	Given I am on the login page
    When they attempt to log in with an email address but no password
	Then they should see an error for the missing password

Scenario: Login to My Account with a password but no email address
	Given I am on the login page
    When they attempt to log in with a password but no email address
	Then they should see an error for the missing email address

Scenario: Unregistered My Account user attempts to log in
    Given I am on the login page
    And I am an unregistered My Account user
    When I log in with invalid credentials
    Then I should see an error message
    Then I should not be logged in