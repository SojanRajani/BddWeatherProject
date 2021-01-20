Feature: WeatherForecastTest
	As a user of Weather Website
    I want to be able to see current temperature
    when I enter Location


  Scenario Outline: User sees current temperature for location of his choice
    Given the google url <baseurl>
    When I enter search string as : Current temperature of Location <Location>
    Given Open Weather Api Endpoint
    When I give Latitude <Latitude> and Longitude <Longitude> 
    Then the current temperature shown as <Temperature>
    Examples:
     | Location | baseurl        | Latitude | Longitude | Temperature |
     | Cochin   | www.google.com | 9.9312   | 76.2673   |    25       |
     | Kerala   | www.google.com | 10.8505  | 76.2711   |    25       |
     | London   | www.google.com | 51.5074  | 0.1278    |    5       |