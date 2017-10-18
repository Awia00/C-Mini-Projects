package com.company;

public class Main {

    public static void main(String[] args) {
        LatexMathParser parser = new LatexMathParser();
        String result = parser.convertToEnglish("\\sum_{p \\in \\Omega} * x^2");
        System.out.println(result);
    }


}
