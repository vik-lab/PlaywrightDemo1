@lakeLevels
Feature: lakeLevels page

As a user
I would like to navigate to the lakeLevels page 
So that I can see the lakeLevels information at any given time

Scenario: Navigate to the lakeLevels page
	Given a user is on the homePage
	When they click the link to navigate to the lake levels page
	Then they should be able to sucessfully open the lake levels page
