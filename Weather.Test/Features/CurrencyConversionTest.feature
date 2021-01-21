Feature: CurrencyConversionTest
	Currency conversion in google search should matches Any Open source API
     
@CheckExchangeRate
    Scenario Outline: Conversion between two currencies
    Given Call Google home URL
    Then Enter search box text: Convert <baseCurrency> to <toCurrency>

    Then I call Currency Convertor with <baseCurrency> and <toCurrency>
    Then the conversion rate should be equal
    Examples:
         | baseCurrency | toCurrency  |
         |     INR      |     USD     | 
         |     EUR      |     INR     |
         |     AUD      |     PLN     |
