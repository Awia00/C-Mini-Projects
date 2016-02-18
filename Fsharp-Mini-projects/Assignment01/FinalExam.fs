module FinalExam
// Name: Anders Wind Steffensen, Date of Birth: 10-02-93, Course: Functional Programming
// The answers by Anders Wind Steffensen to the questions in the exam set in Functional programming (02-06-15), is given in the following .fs file.
// All questions of a problem is given inside the module of that problem.
//
// The solutions are tested in F# interactive. Unless other is stated, F# interactive was able to execute the code.

// Problem 1
module problem1 =
    type multimap<'a,'b when 'a:comparison> = 
        MMap of Map<'a,list<'b>>
    let ex = MMap (Map.ofList [("record",[50]);("ordering",[36;46;70])])
    // Question 1.1
    // I declare the different students.
    let grete = ("Grete",[])
    let hans = ("Hans",["TOPS";"HOPS"])
    let peter = ("Peter",["IFFY"])
    let sine = ("Sine",["HOPS";"IFFY";"BFNP"])
    
    // I create a method to initiate the multimap while holding the invariant that the keys must be sorted.
    let createMM (list:list<('a*list<'b>)>) : multimap<'a,'b>= MMap (Map.ofList (List.sortBy (fun (x,y) -> x) list))

    // I use the multimap creator with the list of the the pairs declared above.
    let studReg = createMM [grete;hans;peter;sine]
    // to declare a version with simular elements but not equal, I change the order of the elements in the valuepair.
    let studReg2 = createMM [grete;("Hans",["HOPS";"TOPS"]);peter;sine]
    
    // Question 1.2
    // I match mm with MMap m to be able to use the .Net Map functions. Then I go over each value in the map (as a list using pattern matching), and sort them
    // at last I use my createMM method to make sure that the initial invariant is kept.
    let canonical (mm:multimap<'a,'b>) : multimap<'a,'b>
        when 'a:comparison and 'b:comparison = 
            match mm with MMap m -> let rec canonicalList (l:('a*list<'b>)list) : ('a*list<'b>)list
                                        when 'a:comparison and 'b:comparison = 
                                        match l with 
                                        | []    -> []
                                        | (y,x)::xs -> (y,(List.sort x))::canonicalList xs
                                    createMM (canonicalList (Map.toList m))
    
    // I use the exact same function just not creating the map in the end.
    let toOrderedList (mm:multimap<'a,'b>) : ('a*'b list) list
        when 'a:comparison and 'b:comparison = 
            match mm with MMap m -> let rec canonicalList (l:('a*list<'b>)list) : ('a*list<'b>)list
                                        when 'a:comparison and 'b:comparison = 
                                        match l with 
                                        | []    -> []
                                        | (y,x)::xs -> (y,(List.sort x))::canonicalList xs
                                    (canonicalList (Map.toList m))
    let studRegOrdered = toOrderedList studReg
    
    // Question 1.3
    // i create a new MMap using the map.empty. the compiler gives a warning.  Value restriction. The value 'it' has been inferred to have generic type val it : multimap<'_a,'_b> when '_a : comparison    
    let newMultimap (u:unit) :multimap<'a,'b> 
        when 'a: comparison = MMap (Map.empty:Map<'a,'b list> when 'a:comparison)
    
    // I change the multimap to a list and then I find the length of that list, and using fold i sum the length of all the lists in the valuepair
    let sizeMultimap (mm:multimap<'a,'b>) : (int*int) = 
        let list = toOrderedList mm
        let a = list.Length
        let b = List.fold (fun x (z,y) -> (List.length y) + x) 0 list
        (a,b)

    // Question 1.4
    // I use pattern matching to go through the map as a list. If i find no element that matches k then i add it. If the element k already exists then i check if v exists in its values.
    // I end up using createMM to ensure that the first invariant is kept since it must be hold, but i dont mind the order of the elements in the 'b list. 
    let addMultimap (k:'a) (v:'b) (m:multimap<'a,'b>) : multimap<'a,'b>
        when 'a : comparison and 'b:equality = 
            let rec inner (l:('a*'b list)list):('a*'b list)list = 
                match l with
                | []         -> [(k,[v])] // no element k exists in the list and it gets added
                | (x,y)::xs  -> if (x=k) 
                                    then (if (List.exists (fun element -> element=v) y)=true 
                                            then (x,y)::xs // nothing happens since the element exists
                                            else (x,v::y)::xs) // the value v gets added to the y list.
                                    else (x,y)::inner xs
            createMM (inner (toOrderedList m))
    
    let test1 = sizeMultimap (addMultimap "Sine" "BFNP" studReg) = (4,6)
    let test2 = sizeMultimap (addMultimap "Grete" "TIPS" studReg) = (4,7)
    let test3 = sizeMultimap (addMultimap "Pia" "" studReg) = (5,7)
    
    // I use a simular structure as the addMultimap with the exception of using map.remove functionality if vOpt is None.
    // If vOpt is some then i use filter to remove it if k exists.
    let removeMultimap (k:'a) (vOpt:'b option) (m:multimap<'a,'b>) : multimap<'a,'b>
        when 'a : comparison and 'b:equality = 
            match m with MMap map -> if (vOpt=None)
                                        then MMap (Map.remove k map)
                                        else let rec inner (l:('a*'b list)list):('a*'b list)list = 
                                                 match l with
                                                 | []         -> [] // no element k exists in the list and nothing happens
                                                 | (x,y)::xs  -> if (x=k) 
                                                                     then (x,(List.filter (fun element -> element<>(Option.get vOpt)) y))::xs
                                                                     else (x,y)::inner xs
                                             createMM (inner (toOrderedList m))
    
    let test4 = sizeMultimap (removeMultimap "Sine"     None          studReg) = (3,3)
    let test5 = sizeMultimap (removeMultimap "Sine"     (Some "PLUR") studReg) = (4,6)
    let test6 = sizeMultimap (removeMultimap "Kenneth"  (Some "BLOB") studReg) = (4,6)
    let test7 = sizeMultimap (removeMultimap "Peter"    (Some "IFFY") studReg) = (4,5)
    // Question 1.5
    // I use the Map and list .net map methods to map the function f onto all value elements of all keys.
    let mapMultimap (f:'a->'b->'c) (m:multimap<'a,'b>) : multimap<'a,'c> 
        when 'a : comparison = match m with MMap asMap -> MMap (Map.map (fun x y -> List.map (f x) y) asMap)
    let test8 = mapMultimap (fun k v -> v+"-F2015") studReg 
    // Question 1.6
    // This function unfortunately does not work 
    let foldMultimap (f:'s->'k->'t->'s) (s:'s) (m:multimap<'k,'t>) : 's 
        when 'k : comparison = match m with MMap asMap -> Map.fold (fun x y -> x + (List.fold (fun i z -> i + (f s z)) y)) s asMap

    //let test9 = foldMultimap (fun acc k v -> String.length v + acc) 0 studReg = 24

// Problem 2
module problem2 =
    let rec f i j xs =
        if xs = [] then 
            [i*j]
        else 
            let (x::xs') = xs
            x*i :: f (i*j) (-1*j) xs'
    let test = f 10 1 [1..9]
    // Question 2.1
    // f computes the elements in xs times i where i = (i)*(-1^(index+1)*j) for index > 1 for index = 0 the i = input i 
    // the list always ends with one last element i*j
    let rec fMatch i j xs = 
        match xs with
        | []        -> [i*j]
        | x::xs'    -> x*i :: fMatch (i*j) (-1*j) xs'
    let test2 = fMatch 10 1 [1..9]
    // Question 2.2
    // I add an inner function which takes the accumulating parameter which must be set to the emptylist initially.
    // when the list parameter is empty the function is done and can return the accumulated parameter.
    let fMatchA i j xs = 
        let rec inner i j list acc = 
            match list with
            | []        -> acc
            | x::xs'    -> inner (i*j) (-1*j) xs' (x*i ::acc)
        inner i j xs []
    let test3 = fMatch 10 1 [1..9]

// Problem 3
module problem3 =
    // Question 3.1
    let myFinSeq n m = seq {for i in [n..m] do yield [n .. i]}
    // myFinSeq computes the sequence of lists where the numbers of the list are n..(index+i) ending in n..m
    // if m<n then the result is the empty sequence

    // Question 3.2
    // I use the concat function to get a single sequence of all the lists in myFinSeq.
    let myFinSeq2 n m = Seq.concat (myFinSeq n m)
    // Question 3.3
    let sum xs = List.fold (fun r x -> r+x) 0 xs
    let seq4000 = myFinSeq 10 4000
    let array4000 = Array.ofSeq seq4000

    let amtOfList = Array.length array4000
    // the amounts of lists in array4000 must be equal to its size since each element in the list is a list.
    let sums = Array.map sum array4000
    // Array.Parallel library has a map function which i use. I get approximately half the real computation time
    let sumsParallel = Array.Parallel.map sum array4000

    let bla = 0

// Problem 4
module problem4 =
    type JSONlite = 
        Object of list<string*Value>
    and Value = 
        | String of string
        | Record of JSONlite
        | Label of string * Value
        | Ref of string
    
    let address = Object [("Street", String "Hansedalen");("HouseNo", String "27")]
    let person1 = Object [("Name", String "Hans Pedersen"); ("Address", Label ("Addr1",Record address))]
    let person2 = Object [("Name", String "Pia Pedersen");  ("Address", Ref "Addr1")]
    let persons = Object [("Person1", Record person1);("Person2", Record person2)]
    // Question 4.1
    // by following the standard given i create a course since it maps to an object which has fields and i use a label to map to it from the student.
    let course = Object[("BFNP", String "10");("BPRP", String "7")]
    let student = [("Name", String "Per Simonsen");  ("Field", String "BSWU"); ("Course", Label ("Course1", Record course))]

    // Question 4.2
    let nl = System.Environment.NewLine
    let space n = String.replicate n " "
    let qqQuote s = "\"" + s + "\""

    // I fill out the given function and use recoursion and the nl space and qqQuote functions
    let ppJSONlite json = 
        let rec ppValue' indent = function 
            | String xs     ->  qqQuote xs + ","
            | Record r      ->  ppJSONlite' (indent+2) r
            | Label (s,l)   ->  s + " -> " + ppValue' (indent+2) l
            | Ref s         ->  "ref " + s
        and ppJSONlite' indent = function
            Object xs -> "{" + List.fold (fun x (s,v) -> x + nl + (space indent) + (qqQuote s) + " : " + ppValue' (indent) v) "" xs  + nl + (space (indent-2)) + "},"
        nl + ppJSONlite' 2 json
    
    // Question 4.3
    // I use the same structure as above and use recoursion to build the map.
    // the function should have no side effects.
    let buildEnv (json:JSONlite) : Map<string,Value> = 
        let rec ppValue' (env:Map<string,Value>) = function 
                | String xs     -> env
                | Record r      -> ppJSONlite' env r
                | Label (s,l)   -> Map.add s l env
                | Ref s         -> env
            and ppJSONlite' (env:Map<string,Value>)= function
                Object xs -> 
                    match xs with
                    | [] -> env
                    | (s,v)::xs -> ppJSONlite' (ppValue' env v) (Object xs)
        ppJSONlite' Map.empty json

    // Question 4.4
    // by going over all the values and if they are ref's changing them to the value in the environment - all refs are changed.
    let expandRef (json:JSONlite) : JSONlite = 
        let env = buildEnv json
        let rec ppValue' = function 
                | String xs     -> String xs
                | Record r      -> match r with Object xs -> Record (Object (List.map (fun (s,x) -> (s,(ppValue' x))) xs))
                | Label (s,l)   -> Label (s,l)
                | Ref s         -> Map.find s env
        match json with Object xs -> Object (List.map (fun (s,x) -> (s,(ppValue' x))) xs)