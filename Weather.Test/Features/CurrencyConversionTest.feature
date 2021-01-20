Feature: CurrencyConversionTest
	Currency conversion in google search should matches Any Open source API

@mytag
Scenario: User sees current rates after currency conversion of currencies of his choice
	Given the google url <baseurl>
    When I enter search string as : <baseCurrency> to <toCurrency> current rates
    Given Currency converter Api Endpoint
    Then the current rates shown as <rates>
    Examples:
     | baseCurrency | toCurrency  | baseurl         | rates | 
     |     INR      |     USD     | www.google.com  | 0.01 | 
     |     EUR      |     INR     | www.google.com  | 88.37 | 
     |     AUD      |     PLN     | www.google.com  | 2.89  |   