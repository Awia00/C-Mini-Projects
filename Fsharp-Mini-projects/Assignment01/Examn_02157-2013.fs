module Examn_02157_2013

// Problem 1
module problem1 =
    type Multiset<'a when 'a : equality> = ('a * int) list
    let okSet = [("b",3);("a",5);("d",1)]
    let failSet = [("b",1);("d",0);("d",1)]
    
    // Question 1
    let rec inv (ms:Multiset<'a>) : bool =
        match ms with
        | ([]) -> true
        | ((x,amt)::xs) -> if(amt>0 && not (List.exists (fun (e,a) -> e=x ) xs)) 
                           then inv xs
                           else false

    // Question 2
    let rec insert e n ms : Multiset<'a> = 
        match ms with
        | ([]) -> [e,n]
        | ((x,amt)::xs) when x<>e -> (x,amt)::(insert e n xs)
        | ((x,amt)::xs) when x=e  ->(x,amt+n)::xs
    
    // Question 3
    let rec numberOf e ms = 
        match ms with
        | ([]) -> 0
        | ((x,amt)::xs) when x<>e -> numberOf e xs
        | ((x,amt)::xs) when x=e  -> amt
    
    // Question 4
    let rec delete e ms : Multiset<'a> = 
        match ms with
        | ([]) -> ms
        | ((x,amt)::xs) when x<>e -> (x,amt)::(delete e xs)
        | ((x,1)::xs) when x=e  -> delete e xs
        | ((x,amt)::xs) when x=e  -> (x,amt-1)::(delete e xs)
    
    // Question 5
    let union (ms1:Multiset<'a>) (ms2:Multiset<'a>) : Multiset<'a> = 
        let totalSet = List.append ms1 ms2
        let rec clean ms : Multiset<'a> =
            match ms with
            | [] -> ms
            | (x,amt)::xs -> let filteredList = (List.filter (fun (e,a) -> e=x) ms)
                             if (filteredList.Length = 0)
                             then if(amt>0) then (x,amt)::clean xs else clean xs
                             else 
                                let newAmt = List.sumBy (fun (e,a) -> a) filteredList
                                (x, newAmt)::clean (List.filter (fun (e,a) -> e<>x) ms)
        clean totalSet

    // Question 6
    type MultisetMap<'a when 'a : comparison> = Map<'a,int>
    let invMM (ms:MultisetMap<'a>) : bool = Map.isEmpty (Map.filter (fun key value -> value<0) ms)
    let insertMM e n (ms:MultisetMap<'a>) : (MultisetMap<'a>) = Map.ofList (insert e n (Map.toList ms))
    let unionMM (ms1:MultisetMap<'a>) (ms2:MultisetMap<'a>) : (MultisetMap<'a>) = Map.ofList (union (Map.toList ms1) (Map.toList ms2)) 

// Problem 2
module problem2 =
    
    let rec f i = function 
        | [] -> []
        | x::xs -> (i,x)::f (i*i) xs

    type 'a Tree = 
        | Lf
        | Br of ('a Tree * 'a * 'a Tree)

     let rec g p = function
     | lf                   -> None
     | Br (_,a,t) when p a  -> Some t
     | Br (t1,a,t2)         -> match g p t1 with
                                | None -> g p t2
                                | res -> res
     // Question 2.1
     (*
     f calculates a list of pairs of i times itself (index+1) times, and the element in the input list.

     *)

    // Question 2.1.1
    let fAcc (i:int) (list:'a list) : list<int*'a>= 
        let rec fa i (input:'a list) (acc:list<int*'a>) = match input with 
           | [] -> acc
           | x::xs -> fa (i*i) xs (acc @ [(i,x)])
        fa i list List.empty<int*'a>

    // Question 2.1.2
    let rec fc i k = function 
       | [] -> k []
       | x::xs -> fc (i*i) (fun y -> k ((i,x)::y)) xs

    //Question 2.1.3
    // I like the continuation-based version the most, since a reversion of the list is not neccessary. The function does not seem to quickly use up stack memory either.

// Problem 3
module problem3 =
    
    let bla = 0

// Problem 4
module problem4 =
    
    let bla = 0