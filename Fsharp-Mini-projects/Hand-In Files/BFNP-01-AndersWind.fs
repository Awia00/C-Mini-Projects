// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
module Assignment01
// *
    // Exercise 1.1
// * 
let sqr x = x*x

    //Exercise 1.2
let pow x n = System.Math.Pow(x,n)

// *
    // Exercise 1.3 : HR 1.1
// *
let g n = n+4

// *
    // Exercise 1.4 : HR 1.2
// *
let h(x,y) = System.Math.Sqrt(x*x+y*y)

// *
    // Exercise 1.5 : HR 1.4
// *
let rec f = function
          | 0 -> 0
          | n -> n + f(n-1)
// f 4 = 10

// *
    // Exercise 1.6 : HR 1.5
// *
let rec fibo = function
               | 0 -> 0
               | 1 -> 1
               | n -> fibo(n-1) + fibo(n-2)
// fibo 4 = 3

// *
    // Exercise 1.7 : HR 1.6
// *
let rec sum = function
                | (m,0) -> m
                | (m,n) -> m + n + sum(m,n-1)

//RECOURSION FORMULAE
// sum (m,n) is defined by 
// (m,0) = m
// (m,n) = m + n + sum(m,n-1)

// *
    // Exercise 1.8 : HR 1.7
// *
// (float * int)
// int
// float
// ((float * int) -> float) * (int -> int))

// *
    // Exercise 1.9 : HR 1.8
// *
// [ a -> 5                 ]
// [ f -> fun a -> a + 1    ]
// [ g -> fun b -> f b + a  ]

// f 3 = 4
// g 3 = 9

// *
    // Exercise 1.10
// *
let dup (a:string) = a + a

// *
    // Exercise 1.11
// *
let rec dupn a n = match a, n with
          | a, 0 -> ""
          | a, n -> a + dupn a (n-1)

// *
    // Exercise 1.12 - Man kunne udnytte minutes funktionen i denne i stedet for den anden vej omkring.
// *
let timediff (h1,m1) (h2,m2) = (h2*60+m2) - (h1*60+m1)

// *
    // Exercise 1.13
// *
let minutes (hh,mm) = hh*60+mm

// *
    // Exercise 1.14 : HR 2.2
// *
let rec pow2(s, n) = match (s,n) with
          | (s,0) -> ""
          | (s,n) -> s + pow2(s, n-1)

// *
    // Exercise 1.15 : HR 2.8
// *
let rec bin(n,k) =  if n<0 || k<0 || k>n then 0
                    else match(n,k) with
                    | (_,0) -> 1
                    | (n,k) when n=k -> 1
                    | (n,k) -> bin(n-1,k-1) + bin(n-1, k)

// *
    // Exercise 1.16 : HR 2.9
// *
//let rec f = function
//            | (0,y) -> y
//            | (x,y) -> f(x-1, x*y);;

// 1. f = int * int -> int
// 2.  The function terminates when x is equal to 0. The function will terminate for all tuples where x is equal to or greater than 0
// 3.  f(2-1,2*3)
//     f(1-1,1*6)
//     f(0,6)
//     6
// 4. y * x * (x-1) * (x-2) * ... * 1 = y * x!

// *
    // Exercise 1.17 : HR 2.10
// *
// 1. (bool,int) -> int
// 2. Stack overflow. This is because the tuple (bool * int) is created before the test function evaluates. And since fact -1 must evaluate to fill the second component of the tuple, the stack overflow happens.
// 3. The result is 0, since the factiorial function is never called before the 'then' expression evaluates (which it never does in this case)

// *
    // Exercise 1.18 : HR 2.13
// *
// curry f is the function g where g x is the function h where h y = f(x, y).
let curry func = fun a b -> func(a,b)
// uncurry g is the function f where f(x, y) is the value h y for the function h = g x.
let uncurry func = fun (a,b) -> func a b
