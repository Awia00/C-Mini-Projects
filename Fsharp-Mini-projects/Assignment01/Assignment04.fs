module Assignment04

// † 
// *
    // Exercise 4.1 : HR 9.1
// *
let xs = [1;2]
let rec g = function
    | 0 -> [1;2]
    | n -> let ys = n::(g(n-1))
           List.rev ys
// Unfortunately i dont fully understand the question :(

// *
    // Exercise 4.2 : HR 9.3
// *
let rec sum = function
                | (0,m) -> m
                | (n,m) -> sum(n-1,n+m)

// *
    // Exercise 4.3 : HR 9.4
    // One iterative declaration is enough
// *
let rec length = function
    |([],n)    -> n
    |(x::xs,n) -> length(xs,n+1)

// *
    // Exercise 4.4 : HR 9.6
// *
let rec factCon s c = match s with
    | 0 -> c 1
    | n -> factCon (n-1) (fun s -> c(s*n))

// *
    // Exercise 4.5 : HR 8.6
    // This to be used in the next task
// *

let fiboWhile n = let mutable a = 0
                  let mutable b = 1
                  let mutable i = 0
                  while i<n do 
                     let temp = a
                     a <- b
                     b <- (temp + b)
                     i <- i+1
                  a

// *
    // Exercise 4.6 : HR 9.7
// *
// 1)
let rec fiboA n n1 n2 = 
    match n with
    | 0 -> 1
    | 1 -> n1+n2
    | _ -> fiboA (n-1) n2 (n1+n2)

// 2)
let rec fiboC n (f:int->int)=
    match n with
    | 0 -> f 0
    | 1 -> f 1
    | _ -> fiboC (n-1) (fun l -> fiboC (n-2) (fun r -> f (l+r)))

// 3)
for i in 1 .. 10000000 do let _ = (fiboWhile 46) in () 
// Real: 00:00:00.518, CPU: 00:00:00.515, GC gen0: 0, gen1: 0, gen2: 0
for i in 1 .. 10000000 do let _ = (fiboA 46 1 0) in ()
// Real: 00:00:00.685, CPU: 00:00:00.687, GC gen0: 0, gen1: 0, gen2: 0
for i in 1 .. 10000000 do let _ = (fiboC 46 id) in ()
// this one takes too long time to run for numbers higher than 40 - seems there is something wrong with it.


// *
    // Exercise 4.7 : HR 9.8
// *
type 'a BinTree =
        | Leaf
        | Node of 'a * 'a BinTree * 'a BinTree

let rec countA a tree = match tree with 
                        | Leaf -> a
                        | Node (n,treeL,treeR) -> countA (countA (a+1) treeL) treeR

let rec count = function
    | Leaf -> 0
    | Node(n, tl, tr) -> (count tl + count tr + 1)

// it is not tail recoursive since we have two trees, so the function has to return from the inner recoursion.

// *
    // Exercise 4.8 : HR 9.9
// *
let rec countC t c =
    match t with
    | Leaf -> c 0
    | Node(n,tl,tr) ->
        countC tl (fun vl -> countC tr (fun vr -> c(vl+vr+1)))

let rec countAC tree (a:int) c = 
    match tree with 
    | Leaf                 -> (c a)
    | Node (_,treeL,treeR) -> countAC treeL (a+1) (fun r -> countAC treeR r c)

// *
    // Exercise 4.9 : HR 9.10
// *
let rec bigListK n k =
    if n=0 then k []
    else bigListK (n-1) (fun res -> 1::k(res))

// The problem here is 1::k(res) since the stack needs to keep 1::(something) in the stack until k(res) has returned, when it gets executed.

// *
    // Exercise 4.10 : HR 9.11
// *
let rec leftTree (tree:BinTree<int>) n= 
    match n with
    | 0 -> tree
    | _ -> leftTree (Node(n, tree, Leaf)) (n-1)

let rec rightTree (tree:BinTree<int>) n= 
    match n with
    | 0 -> tree
    | _ -> rightTree (Node(n, Leaf, tree)) (n-1)
// 1)
(*
The leftTree creation creates out of memory with values above 30 000 000
The rightTree creation creates out of memory with values above 30 000 000

count creates a stack overflow with a rightTree around the size of 70 000
count creates a stack overflow with a leftTree around the size of 70 000

I cannot create a big enough rightTree to create a stack Overflow for countA
CountA creates a stack overflow with a leftTree around the size of 70 000
*)
// 2)
(*
I cannot create a big enough rightTree to create a stack Overflow for countC or countAC
I cannot create a big enough leftTree to create a stack Overflow for countC or countAC

For countC to finish for a 30 000 000 rightTree it takes: Real: 00:00:07.549, CPU: 00:00:07.546, GC gen0: 155, gen1: 91, gen2: 0
For countAC to finish for a 30 000 000 rightTree it takes: Real: 00:00:01.513, CPU: 00:00:01.515, GC gen0: 229, gen1: 1, gen2: 0

For countC to finish for a 30 000 000 leftTree it takes: Real: 00:00:07.692, CPU: 00:00:07.687, GC gen0: 304, gen1: 75, gen2: 1
For countAC to finish for a 30 000 000 leftTree it takes: Real: 00:00:07.433, CPU: 00:00:07.406, GC gen0: 77, gen1: 74, gen2: 1
*)

// *
    // Exercise 4.11 : HR 11.1
// *
let oddNumbers = Seq.initInfinite(function 
    | 1 -> 1
    | n -> n*2-1)

// *
    // Exercise 4.12 : HR 11.2
// *
let factNumbers = Seq.initInfinite(function
    | 0 -> 1
    | 1 -> 1
    | n -> n*(n-1))