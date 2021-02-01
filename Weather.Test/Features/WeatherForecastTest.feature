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
     | Alappuzha     | 9.4981   | 76.3388  |

  #"dd/MM/yyyy h:mm:ss tt" format
  Scenario Outline: User sees hourly temperature for location of his choice
    Given I Call the google home page
    When I enter search as : Current temperature of Location <Location> at Date Time <Time>
    
    Given I call hourly Open weather api with Latitude <Latitude> and Longitude <Longitude> 
    Then the current temperatures should be equal
    Examples:
     | Location   | Latitude | Longitude | Time                  |
     | Cochin     | 9.9312   | 76.2673   | 31/01/2021 5:00:00 AM |
     | Trivandrum | 8.5241   | 76.9366   | 31/01/2021 6:00:00 PM |
     | Alappuzha  | 9.4981   | 76.3388   | 31/01/2021 7:00:00 PM |