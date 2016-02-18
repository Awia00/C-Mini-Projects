module Examn_jun2013

// Problem 1
module Problem1 =
    type item = {id : int; name: string; price:float}
    type register = item list
    // Question 1.1
    // I first declare the individual items
    let item1 = {id = 1; name = "Milk"; price = 8.75}
    let item2 = {id = 2; name = "Juice"; price = 16.25}
    let item3 = {id = 3; name = "Rye Bread"; price = 25.00}
    let item4 = {id = 4; name = "White Bread"; price = 18.50}
    // then since register is a list of items, I declare register 1 to be the list of item1 to item4
    let register1 = [item1;item2;item3;item4]
    

    // Question 1.2
    exception Register of string;
    // i use patternmatching an recoursion to go through the list until i match the id. 
    // If i end up with an empty list - no element was found and therefore the exception must be raised.
    let rec getItemById (i:int) (r:register) :item = 
        match r with
        | []    -> raise (Register ("no item with ID " + string i))
        | x::xs -> if x.id=i then x else getItemById i xs

    // Question 1.3
    // I use List.maxBy to find the element with the highest id and then add 1 to that id.
    let rec nextId (r:register) : int = (List.maxBy (fun x -> x.id) r).id+1

    // Question 1.4
    // i use the nextId to find the next Id. Then I append the list with the new element to the existing register. 
    // That way the register "can" be in order.
    let addItem (n:string) (p:float) (r:register) : register = r@[{id=nextId r; name=n; price=p}]

    // Question 1.5
    // I use pattern matching and recoursion to go over the list. If it finds the element then it returns the rest of the list. 
    // each recoursion cons the rest of the recoursion on if the element was not found.
    // therefore this method only deletes 1 element with the id = i.
    let rec deleteItemById (i:int) (r:register) : register = 
        match r with
        | []    -> []
        | x::xs -> if x.id = i then xs else x::(deleteItemById i xs)
    
    // Question 1.6

    let registerBad = [item1;item2;item3;item3]
    // By using pattern matching and recoursion we can go over all elements. Each element has its Id checked up against all other elements in the rest of the list.
    // if the amount of elements in the rest of the list, with the same id, is over 0 then the register cannot be unique.
    let rec uniqueRegister (r:register) : bool = 
        match r with
        | []    -> true
        | x::xs -> if List.length (List.filter (fun y -> y.id=x.id) xs)>=1 then false else uniqueRegister xs

    // Question 1.7
    // I use the filter list function, to get the list only with items with prices between p-d inclusive and p+d inclusive
    let itemsInPriceRange (p:float) (d:float) (r:register) :register = List.filter (fun x -> x.price >= p-d && x.price <= p+d) r

// Problem 2
module Problem2 =
    let rec f n m =
        if m=0 then n
        else n * f (n+1) (m-1)

    // Question 2.1
    // f is of type  int -> int -> int
    // f computes n*n+1*n+2 ... *n+m 

    // Question 2.2
    // I created a continue based function to solve the problem.
    // the function must be given the Id function as the parameter k
    let rec fcon n m k =
        if m=0 then k n
        else fcon (n+1) (m-1) (fun x -> x * k n)
     
    // this is the accumulating parameter version.
    // the function must be given 1 as the parameter acc
    let rec facc n m acc =
       if m=0 then n*acc
       else facc (n+1) (m-1) (n*acc)


    let rec z xs ys =
        match (xs, ys) with
        ([],[]) -> []
        | (x::xs,[]) -> (x,x) :: (z xs ys)
        | ([],y::ys) -> (y,y) :: (z xs ys)
        | (x::xs,y::ys) -> (x,y)::(z xs ys)

    // Question 2.3
    // The type of z  is 'a list -> 'a list -> ('a * 'a) list
    // the function computes the list of pairs between the two. Often called a zip function.
    // in cases where the one of the lists are longer than the other, the elements in the long list is just paired with itself.
    //
    //  Example 1
    // z ["a";"c"] ["b";"d"];;
    // val it : (string * string) list = [("a", "b"); ("c", "d")]
    //  Example 2
    // z ["a"] ["b";"d";"f"];;
    // val it : (string * string) list = [("a", "b"); ("d", "d"); ("f", "f")]
        


    let rec s xs ys =
        match (xs,ys) with
        ([],[]) -> []
        | (xs,[]) -> xs
        | ([],ys) -> ys
        | (x::xs,y::ys) -> x::y::s xs ys
        
    // Question 2.4
    // The type of z  is 'a list -> 'a list -> 'a list
    // The function computes the list of elements in both list, taken in the order of index in each list, with the elements of xs first if the index is equal.
    //  
    //  Example 1
    // s ["a";"c"] ["b";"d"];;
    // val it : string list = ["a"; "c"; "b"; "d"]
    //  Example 2
    // s ["a"; "c"] ["b";"d";"e";"f"];;
    // val it : string list = ["a"; "b"; "c"; "d"; "e"; "f"]

    // Question 2.5
    // the function must be given id to the k parameter.
    let rec s' xs ys k =
        match (xs,ys) with
        ([],[]) -> k []
        | (xs,[]) -> k xs
        | ([],ys) -> k ys
        | (x::xs,y::ys) -> s' xs ys (fun z -> k(x::y::z))

// Problem 3
module Problem3 =
    type Latex<'a> =
        | Section of string * 'a * Latex<'a>
        | Subsection of string * 'a * Latex<'a>
        | Text of string * Latex<'a>
        | End
    
    let text1 = Section ("Introduction", None,
                    Text ("This is an introduction to ...",
                        Subsection ("A subsection", None,
                            Text ("As laid out in the introduction we ...",
                                End))))
    
    let text2 = Section ("Introduction", None,
                    Text ("This is an introduction to ...",
                        Subsection ("A subsection", None,
                            Text ("As laid out in the introduction we ...",
                                Subsection ("Yet a subsection", None,
                                    Section ("And yet a section", None,
                                        Subsection ("A subsection more...", None,
                                            End)))))))
    
    // Question 3.1
    // the type of text1 is Latex<'a option> (with a bunch of subelements)

    // Question 3.2
    let addSecNumbers (input:Latex<'a>) : Latex<string> = 
        let rec secNumbers (t:Latex<'a>) secNumber subSecNumber : Latex<string> =
            match t with
            | Section (s,value,element)     -> Section (s, string (secNumber+1), secNumbers element (secNumber+1) 0)
            | Subsection (s,value,element)  -> Section (s, string secNumber + "." + string (subSecNumber+1), secNumbers element secNumber (subSecNumber+1))
            | Text (s,element)              -> Text (s, secNumbers element secNumber subSecNumber)
            | End                           -> End
        secNumbers input 0 0

    // Question 3.3
    // addSecNumbers is of type Latex<'a> -> Latex<string>

    
    
    type NewLatex<'a> =
        | Section of string * 'a * NewLatex<'a>
        | Subsection of string * 'a * NewLatex<'a>
        | Label of string * NewLatex<'a>
        | Text of string * NewLatex<'a>
        | Ref of string * NewLatex<'a>
        | End
    let text3 = Section ("Introduction", "1",
                    Label("intro.sec",
                        Text ("In section",
                            Ref ("subsec.sec",
                                Text (" we describe ...",
                                        Subsection ("A subsection", "1.1",
                                            Label("subsec.sec",
                                                Text ("As laid out in the introduction, Section ",
                                                    Ref ("intro.sec",
                                                        Text (" we ...",
                                                            End))))))))))
 
    // Question 3.4
    let buildLabelEnv (input:NewLatex<'a>) : Map<string,string> =
        let rec inner (latex:NewLatex<string>) (acc:Map<string,string>) (lastSecNumber:string) : Map<string,string> =
            match latex with
            | Section (s,value,element)                 -> inner element acc value
            | Subsection (s,value,element)              -> inner element acc value
            | Label (s, element)                        -> inner element (Map.add s lastSecNumber acc) lastSecNumber
            | Text (s,element)                          -> inner element acc lastSecNumber
            | Ref (s,element)                           -> inner element acc lastSecNumber
            | End                                       -> acc
        inner input Map.empty "" // here input should be called with the new version of addSecNumbers
    // Question 3.5
    let nl = System.Environment.NewLine
    let rec toString (input:NewLatex<'a>) : string = 
        match input with
        | Section (s,value,element)                 -> nl + value + " " + s + nl + toString element
        | Subsection (s,value,element)              -> nl + value + " " + s + nl + toString element
        | Label (s, element)                        -> toString element
        | Text (s,element)                          -> s + toString element
        | Ref (s,element)                           -> "(" + s + ")" + toString element
        | End                                       -> ""

// Problem 4
module Problem4 =
    
    let mySeq = Seq.initInfinite (fun i -> if i % 2 = 0 then -i else i)
    
    // Question 4.1
    // the result value is seq<int> the result is seq [0; 1; -2; 3; ...]

    // Question 4.2
    let finSeq n M = Seq.map (fun x -> n + x*2) [1..M]


    type X = A of int | B of int | C of int * int

    let rec zX xs ys =
        match (xs,ys) with
        (A a::aS,B b::bS) -> C(a,b) :: zX aS bS
        | ([],[]) -> []
        | _ -> failwith "Error"

    let rec uzX xs =
        match xs with
        | C(a,b)::cS -> let (aS,bS) = uzX cS 
                        (A a::aS,B b::bS)
        | [] -> ([],[])
        | _ -> failwith "Error"
    
    // Question 4.3
    // zX is of type X list -> X list -> X list
    // zX creates the list of pairs of the two lists. 
    // It requires them to be the same length and the first list must be of exclusive Xs of type A and the second Xs of type B
    // This functions has traits of a zip function
    
    // uzX is of type X list -> X list * X list
    // Does the opposite of zX. It takes a list of X elements of type C and results in a pair of two lists, with Xs of type A in the first and type B in the second.
    // All elements on uneven indexes will be put in the first and all on even indexes the second.
    // This functions has traits of an unzip function