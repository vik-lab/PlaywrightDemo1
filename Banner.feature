@web @mercuryWebsite
Feature: Banner

As an existing or potential customer
I want to be able to browse the Mercury website
So that I can understand Mercury's offerings

Scenario: Navigate to the Gas page
	Given a user is on the Mercury website home page
	When they click on banner link 'Gas'
	Then they should be taken to the Gas page	

Scenario: Navigate to the Electricity page
	Given a user is on the Mercury website home page
	When they click on banner link 'Electricity'
	Then they should be taken to the Electricity page

Scenario: Navigate to the Broadband page
	Given a user is on the Mercury website home page
	When they click on banner link 'Broadband'
	Then they should be taken to the Broadband page

Scenario: Navigate to the Mobile page
	Given a user is on the Mercury website home page
	When they click on banner link 'Mobile'
	Then they should be taken to the Mobile page