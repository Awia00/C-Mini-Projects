// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

// 1. lektion
let circleArea r = System.Math.PI*r*r
let daysInMonth = function | 2->28 | 4|6|9|11 -> 30 | _ -> 31
let rec fact = function
               | 0->1
               | n->n*fact(n-1)
let rec power = function 
                | (_,0) -> 1.0
                | (x,n) -> x * power(x,n-1)
let rec gcd = function
                | (0,n) -> n
                | (m,n) -> gcd(n%m,m)
// Lister - type er vigtigt
let intList = [1;2;3]
let stringList = ["a";"ab";"abc"]
let funqList = [sin;cos]
let tupleList = [(1,true);(2,false)]
let listList = [[1;2];[1;2;3]]

// 2. lektion

let f2 x = x+2
let g2 y = y*y
let fAndg = f2 << g2        // f2(g2(y)) eller f o g
let f2Andg2 x = f2(g2(x))   // f2(g2(y)) eller f o g
let another foo x = f2(foo x) // den kan selv interefere at faa er en metode.

// kræver en compare metode i dette tilfælde i .net library.
let ordText x y = match compare x y with
                  | t when t>0  -> "greater than"
                  | 0 -> "equal to"
                  | _  -> "less than"


let g x = 
         let a = 6
         let f y = y + a
         x + f x 
// g 1 = 8 

// lister bygger på CONS som svarer til :: tegnene. 
// cons notationen bruges til at splitte eller bygge lister.
 

// sum functioner
let rec sumIntList = function 
                     | [] -> 0 // <- det her er der man siger hvilken type listen skal være
                     | x::xs -> x+sumIntList(xs)
let rec sumStringList = function 
                     | [] -> "" // <- det her er der man siger hvilken type listen skal være
                     | x::xs -> x+sumStringList(xs)
let rec sumFloatList = function 
                     | [] -> 0.0 // <- det her er der man siger hvilken type listen skal være
                     | x::xs -> x+sumFloatList(xs)

let rec (>=.) = function
                | ([],_) -> true
                | (_,[]) -> false
                | (x::xs',y::ys') -> x<=y && xs' >= ys'

// 1.1
let g1 n = n+4
let g3 = (+) 4
// 1.2
let h(x,y) = System.Math.Sqrt(x*x+y*y)


type mySeqBuilder() =
    member this.For (x,xs) = Seq.collect x xs
    member this.Yield x = Seq.singleton x
    member this.Zero = Seq.empty


[<EntryPoint>]
let main argv = 
    printfn "Hello World"
    0;; // return an integer exit code