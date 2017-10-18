package com.company;

import java.util.Hashtable;

public class LatexMathParser {
    private final String openingBrackets = "{[(";
    private final String closingBrackets = "}])";
    private final Hashtable<String,String> operations = new Hashtable<>();

    public LatexMathParser() {
        operations.put("\\sum_", "The sum over");
        operations.put("\\in", "in the set of ");
        operations.put("^", "raised to the power of ");
        operations.put("+", "plus");
        operations.put("-", "minus");
        operations.put("*", "times");
        operations.put("/", "divided by");
        operations.put("%", "modulus");
    }

    private int findLastOpeningBracket(String text) {
        int lastBracketIndex = -1;
        char[] characters = text.toCharArray();
        for (int i = 0; i < characters.length; i++) {
            if(openingBrackets.contains(characters[i]+"")) {
                lastBracketIndex = i;
            }
        }
        return lastBracketIndex;
    }

    private int findFirstClosingBracket(String text, int startingAt) {
        char[] characters = text.toCharArray();
        for (int i = startingAt; i < characters.length; i++) {
            if(closingBrackets.contains(characters[i]+"")) {
                return i;
            }
        }
        return -1;
    }

    private int findMatchingClosing(String text, int startingAt){
        char[] characters = text.toCharArray();
        int countingOpening = 0;
        for (int i = startingAt; i < characters.length; i++) {
            if(openingBrackets.contains(characters[i]+""))
                countingOpening++;
            else if(closingBrackets.contains(characters[i]+"")) {
                if(countingOpening == 0)
                    return i;
                else
                    countingOpening--;
            }
        }
        return -1;
    }

    private String parseOperation(String text, int endingAt) {
        char[] characters = text.toCharArray();
        for (int i = endingAt; i > 0; i--) {
            if(characters[i] == '\\'){
                return text.substring(i, endingAt);
            }
        }
        return "";
    }

    public String firstOperation(String expression){
        final int[] lowestIndex = {expression.length()};
        final String[] result = {""};
        operations.forEach((String s, String s2) -> {
            int index = expression.indexOf(s);
            if(index != -1 && index< lowestIndex[0]){
                lowestIndex[0] = index;
                result[0] = s;
            }
        });
        return result[0];
    }

    public String convertToEnglish(String latexMath) {
        String result = "";
        String expression = latexMath;
        do {
            String operation = firstOperation(expression);
            if(operation.length() != 0) {
                int index = expression.indexOf(operation);
                int closing = findMatchingClosing(expression, index+operation.length()+1);
                if(closing > 0){
                    String inner = expression.substring(index+ operation.length()+1, closing);

                    result += expression.substring(0, index);
                    result += operations.get(operation);
                    result += "(" + convertToEnglish(inner) + ")";

                    expression = expression.substring(closing+1);
                }
                else{
                    result += expression.substring(0,index) + " " + operations.get(operation);
                    expression = expression.substring(index+operation.length());
                }
            } else
            {
                result += expression;
                expression = "";
            }
        } while(expression.length()!=0);
        return result;
    }
}
