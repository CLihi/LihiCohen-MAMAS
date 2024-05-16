using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

class NumericalExpression
{
    public long Number {get; set;}
    public NumericalExpression(long number)
    {
        Number = number;
    }

    private string UnitExpression(int num)
    {
        switch(num)
        {
            case 0: return "";
            case 1: return "one";
            case 2: return "two";
            case 3: return "three";
            case 4: return "four";
            case 5: return "five";
            case 6: return "six";
            case 7: return "seven";
            case 8: return "eight";
            case 9: return "nine";
            default: return "non unit number";
        }
    }

    private string TeenExpression(int unity)
    {
        switch(unity)
        {
            case 0: return "ten";
            case 1: return "eleven";
            case 2: return "twelve";
            case 3: return "thirteen";
            case 4: return "fourteen";
            case 5: return "fifteen";
            case 6: return "sixteen";
            case 7: return "seventeen";
            case 8: return "eighteen";
            case 9: return "nineteen";
            default: return "invalid number";
        }
    }

    private string TensExpression(int tens, int unity)
    {
        switch(tens)
        {
            case 0: return "";
            case 1: return TeenExpression(unity);
            case 2: return "twenty";
            case 3: return "thirty";
            case 4: return "forty";
            case 5: return "fifty";
            case 6: return "sixty";
            case 7: return "seventy";
            case 8: return "eighty";
            case 9: return "ninety";
            default: throw new Exception ("non unit number");
        }
    }

    private string TripleDigitsExpression(int num)
    {
        int units = num%10;
        int tens = (num/10) % 10;
        int hundreds = num/100; 
        if (tens == 1 && hundreds == 0){
            return TeenExpression(units); 
        }
        if (tens == 1){
            return UnitExpression(hundreds)+" hundred " + TeenExpression(units); 
        }
        if (hundreds == 0){
            return TensExpression(tens, units) +" "+ UnitExpression(units); 
        }
        else{
            return UnitExpression(hundreds)+" hundred " + TensExpression(tens, units) +" "+ UnitExpression(units); 
        }

    }
    public override string ToString()
    {
        if (Number == 0) {
            return "zero";
        }
        string expression = Number < 0 ? "minus " : "";
        long abs_num = Math.Abs(Number);
        if (abs_num > 999999999999) {
            throw new Exception($"Can only express numbers between -999999999999 to 999999999999, you've entered {Number}");
        }
        int hundreds = (int) (abs_num%1000);
        int thousands = (int) (abs_num/1000) % 1000;
        int millions = (int) (abs_num/1000000) % 1000;
        int billions = (int) (abs_num/1000000000) % 1000;
        if (billions != 0) {
            expression += TripleDigitsExpression(billions) + " billion, ";
        }
        if (millions != 0) {
            expression += TripleDigitsExpression(millions) + " million, ";
        }
        if (thousands != 0) {
            expression += TripleDigitsExpression(thousands) + " thousand, ";
        }
        if (hundreds != 0) {
            expression += TripleDigitsExpression(hundreds);
        }
        return expression;
    }

    public long GetValue()
    {
        return Number;
    }

    public static long SumLetters(long num)
    {
        long sum = 0;
        NumericalExpression i_expression = new NumericalExpression(0);
        for (int i = 0 ; i <= num ; i ++)
        {
            i_expression.Number = i;
            string expression = i_expression.ToString();
            expression = expression.Replace(" ","");
            sum += expression.Length;
        }
        return sum;
    }

    public long SumLetters()
    {
        long sum = 0;
        NumericalExpression i_expression = new NumericalExpression(0);
        for (int i = 0 ; i <= Number ; i ++)
        {
            i_expression.Number = i;
            string expression = i_expression.ToString();
            expression = expression.Replace(" ","");
            sum += expression.Length;
        }
        return sum;
    }
}