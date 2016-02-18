module Assignment02
// *
    // Exercise 2.1
// * 
exception InputNotValid
let rec downTo (n:int) = if n<0 then raise InputNotValid else 
                         if n = 0 then [] else n::downTo(n-1)

let rec downTo1 n = if n<0 then raise InputNotValid else 
                    match n with
                    | 0 -> [0]
                    | n -> n::(downTo1 (n-1))

// *
    // Exercise 2.2
// * 
let rec removeOddIdx = function
    | []                 -> []
    | x::xs when x%2 = 0 -> x::removeOddIdx(xs)
    | _::xs              -> removeOddIdx(xs)

// *
    // Exercise 2.3
// * 
let rec combinePair = function
    | []        -> []
    | [x1]      -> []
    | x::xs::xk -> (x,xs)::combinePair xk
                   
               

// *
    // Exercise 2.4 : HR 3.2
// * 
module ex24 = 
    let convertToPence (pounds,shilling,pence) = (pounds*12*20) + (shilling*12) + pence
    let convertPenceToOldEnglish n = let pence = n%12
                                     let shilling = ((n-pence)/12)%20
                                     let pounds = ((n-shilling-pence)/12/20)
                                     (pounds, shilling, pence)

    let (.+.) x y = let total = convertToPence x + convertToPence y
                    convertPenceToOldEnglish total
    
    let (.-.) x y = let total = convertToPence x - convertToPence y
                    convertPenceToOldEnglish total
    
// *
    // Exercise 2.5 : HR 3.3
// * 
exception DivideByZero
module ex25 =
    let (.+.) ((a:float), (b:float)) ((c:float), (d:float)) = (a+c,b+d)
    let (.*.) (a:float, b:float) (c:float, d:float) = (a*c - b*d, b*c + a*d)
    let (~-) (a:float,b:float) = (-a,-b)
    let (.-.) (a,b) (c,d) = (a,b) .+. (-(c,d))
    let inverseMulti (a:float,b:float) = if (a=0.0 || b=0.0) then raise DivideByZero else 
                                            let add = a*a+b*b
                                            (a/add, (b*(-1.0))/add)
    let (./.) a b = a .*. (inverseMulti b)

// *
    // Exercise 2.6 : HR 4.4
// * 
let rec altSum = function
    | []    -> 0
    | x::xs ->  x - altSum(xs)

// *
    // Exercise 2.7
// * 
let explode (s:string) = s.ToCharArray() |> List.ofArray

let rec explode2 (s:string) = match s with
    | ""  -> []
    | s   -> s.Chars 0::explode2(s.Remove(0,1))

// *
    // Exercise 2.8
// * 
let implode (a:char list) = List.foldBack (fun x s -> string x + string s) a ""

let implodeRev (a:char list) =  List.fold (fun s x -> string x + string s) "" a

// *
    // Exercise 2.9
// * 
let toUpper (s:string) = implode(List.map(System.Char.ToUpper) (explode s))
let toUpper1 (s:string) = explode s |> List.map(System.Char.ToUpper) |> implode
let toUpper2 (s:string) = implode << List.map(System.Char.ToUpper) <| explode s

// *
    // Exercise 2.10
// * 
let palindrome s = let k = toUpper s |> explode
                   (implode k) = (implodeRev k)

// *
    // Exercise 2.11
// * 
let rec ackermann (m,n) = if m<0 || n<0 then raise InputNotValid
                          else match(m,n) with
                                | (0,n) -> n+1
                                | (m,0) -> ackermann (m-1,1)
                                | (m,n) -> ackermann (m-1,ackermann (m,n-1))
// ackermann (3,11) = 16381

// *
    // Exercise 2.12 : HR 5.4
// * 

let time f =
    let start = System.DateTime.Now in
    let res = f () in
    let finish = System.DateTime.Now in
    (res, finish - start);

let timeArg1 f a = time(fun k -> f a)

// *
    // Exercise 2.13 : HR 5.4
// * 

let rec downTo2 f n e = match n with 
    | _ when n <= 0 -> e
    | n -> downTo2 f (n-1) (f n e)

let factorialDownTo2 n = downTo2 (*) n 1

let consDownTo2 g e = downTo2 (fun x s -> (g x)::s) e []
