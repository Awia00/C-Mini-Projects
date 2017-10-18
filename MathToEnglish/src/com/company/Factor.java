package com.company;

import java.util.List;

public class Factor {
    private List<Factor> children;
    private String expression;

    public Factor(List<Factor> children, String expression) {
        this.children = children;
        this.expression = expression;
    }

    public List<Factor> getChildren() {
        return children;
    }

    public void addChild(Factor child) {
        this.children.add(child);
    }

    public void removeChild(Factor child) {
        this.children.remove(child);
    }

    public String getExpression() {
        return expression;
    }

    public void setExpression(String expression) {
        this.expression = expression;
    }
}
