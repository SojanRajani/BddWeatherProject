Feature: WeatherForecastTest
	As a user of Weather Website
    I want to be able to see current temperature
    when I enter Location


  Scenario Outline: User sees current temperature for location of his choice
    Given I Call the google home page
    When I enter search string as : Current temperature of Location <Location>
  
    Given I call Open weather api with Latitude <Latitude> and Longitude <Longitude> 
    Then the current temperatures should be equal
    Examples:
     | Location     | Latitude | Longitude |
     | Cochin       | 9.9312   | 76.2673   |
     | Trivandrum   | 8.5241   | 76.9366   |
     | Alappuzha     | 9.4981   | 76.3388   |