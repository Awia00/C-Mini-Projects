module Examn_02157_2012


// *
    // Problem 1
// *
module ex1 =
    type Name = string
    type Score = int
    type Result = Name * Score
    let testList = [("Fischer",50);("Wind",100);("Morten",5);("Cecilie",30);("Mikael",99);("Thomas",10);("Wind",100)]
    
    exception InvalidResult
    let funtionOnScore ((n1,s1):Result) ((n2,s2):Result) f = f s1 s2
    
    // 1)
    let legalResult (list:Result list) = not (List.exists (fun (y,x) -> x<0 || x>100) list)

    // 2)
    // I return an option int, since I find it better to return None when the list is empty than some random int
    let rec maxScore (list:Result list) highestResult = 
        match list with
        | []         -> None
        | (_,x)::[]  -> if highestResult<x then Some (x) else Some (highestResult)
        | (_,x)::xs  -> if highestResult<x then maxScore xs x else maxScore xs highestResult
    
    // 3)
    // I return an option Result, since I find it better to return None when the list is empty than some random Result
    let best (list:Result list) =
        let rec inner innerList highestResult =
                match (innerList) with
                | []         -> None
                | [rNext]    -> if funtionOnScore highestResult rNext (<) then Some rNext else Some (highestResult)
                | rNext::xs  -> if funtionOnScore highestResult rNext (<) then inner xs rNext else inner xs highestResult
        inner list ("",0)
    
    // 4)
    let average (list:Result list) = (float (List.foldBack (fun ((x,y):Result) s -> y + s) list 0)) / (float list.Length)
    
    // 5)
    // this is faulty since it removes all results and not just the first found.
    //let delete r rs:Result list = List.filter(fun test -> not(test=r)) rs
    let rec delete r rs:Result list = 
        match rs with 
        | []    -> []
        | x::xs when x = r -> xs
        | x::xs -> x::(delete r xs)
    
    // 6)
    let rec bestN (rs:Result list) n = 
        if (List.length rs)<n 
            then raise InvalidResult 
            else let rec inner fullList x accumulated =
                     match (x) with
                     | 0  -> accumulated
                     | _  ->  let next = Option.get (best fullList)
                              inner (delete next fullList) (x-1) (next::accumulated)
                 inner rs n []

// *
    // Problem 2
// *
module ex2 =
    type Typ = | Integer
               | Boolean
               | Ft of Typ list * Typ

    type Decl = string * Typ

    type SymbolTable = Map<string,Typ>

    type Exp = | V of string
               | A of string * Exp List

    type Stm = 
        | Ass   of string * Exp // assignment
        | Seq   of Stm * Stm // sequential omposition
        | Ite   of Exp * Stm * Stm // if-then-else
        | While of Exp * Stm // while
        | Block  of Decl list * Stm // block
    
//    let testType1 = Ft ([Integer;Integer],Boolean)
//    let testType2 = Ft ([Integer],Boolean)
//    let testDecl1 = ("=", testType1)
//    let testDecl2 = ("0<",testType2)
//    let testDecl3 = ("<0",testType2)
//    let testDecl4 = ("0=",testType2)
//    let testDeclList1 = [testDecl1;testDecl2]
//    let testDeclList2 = [testDecl1;testDecl2;testDecl2]
//    let testDeclList3 = [testDecl3;testDecl4]
//    let testSymbolTable1 :SymbolTable = Map.empty.Add("=",testType1).Add("0<",testType2)
//    let testSymbolTable2 :SymbolTable = Map.empty.Add("x",Integer).Add("y",Integer).Add(">",testType1)
//    let testExp1 = A(">",[V "x"; V"y"])
//    let testExp2 = A(">",[V "z"; V"y"]) 
//    let testExp3 = A("<",[V "x"; V"y"])
//    let testStm1 = (While (testExp1,(Ass ("x",(V "x"))))) // with symbolTable2
//    let testStm2 = (Ite (testExp1,(Seq ((Ass ("x",(V "x"))),(Ass ("x",(V "x"))))), (Ass ("x",(V "x")))))

    // 1)
    let rec distinctVars (declList:Decl list) = 
        match declList with
        | []    -> true
        | x::xs -> if (List.exists (fun n -> n=x) xs) then false else (distinctVars xs)

    // 2)
    let toSymbolTable declList = 
        let rec inner (list:Decl list) map : SymbolTable =
            match list with
            | []    -> map
            | (s,t)::xs -> inner xs (map.Add (s,t))
        inner declList Map.empty

    // 3)
    let rec extendST sym decls : SymbolTable = 
        match decls with
        | [] -> sym
        | (s,t)::xs -> extendST (sym.Add (s,t)) xs

    // 4) 

    let rec symbolsDefined (sym:SymbolTable) (e:Exp) = 
        match e with
        | V s -> sym.ContainsKey s
        | A (s,xs) -> if (sym.ContainsKey s) 
                        then List.forall (symbolsDefined sym) xs
                        else false

    // 5)
    let typOf (sym:SymbolTable) e =
        let form = 
            match e with
                | V s       -> Option.get (sym.TryFind s)
                | A (s,_)   -> Option.get (sym.TryFind s)
        let findResultTyp t =
            match t with
                    | (Ft (_,typ)) -> typ
                    | n -> n
        let rec check (typ:Typ) (exp:Exp)= 
            match (typ,exp) with
                | (Integer, V x) when (Option.get (sym.TryFind x) = Integer)    -> true
                | (Boolean, V x) when (Option.get (sym.TryFind x) = Boolean)    -> true
                | (Ft(typList,t), A(x,expList)) -> let rec checkList (tList:Typ list) (eList:Exp list) : bool =
                                                        match (tList,eList) with
                                                            | [],[] -> true
                                                            | (t1::ts),(e1::es) -> check t1 e1 && checkList ts es
                                                            | _ -> failwith "solo"
                                                   let answer = checkList typList expList
                                                   let answer2 = t = findResultTyp (Option.get (sym.TryFind x))
                                                   answer2 && answer
                | _ -> failwith "yolo"
        if (check form e) 
            then findResultTyp form
            else failwith "rolo"


    // 6)
    // (typOf sym e)=Boolean : check here if the first part is correct (only allows e to be boolean and not list,boolean)
    let rec wellTyped (sym:SymbolTable) (stm:Stm) : bool =
        match stm with
        | Ass (x, e)            -> ((sym.TryFind x)<>None && (Option.get (sym.TryFind x))=(typOf sym e)) && symbolsDefined sym e
        | Seq (stm1, stm2)      -> wellTyped sym stm1     && wellTyped sym stm2
        | Ite (e, stm1, stm2)   -> (typOf sym e)=Boolean  && wellTyped sym stm1 && wellTyped sym stm2 && symbolsDefined sym e
        | While (e,stm)         -> (typOf sym e)=Boolean  && symbolsDefined sym e && wellTyped sym stm
        | Block (list,stm)      -> distinctVars list      && wellTyped (extendST sym list) stm
// *
    // Problem 3
// *
module ex3 = 
    // 1)
    // a is of type 'a list
    // b is of type 'a list
    // h is of type 'a list
    // the function h takes two list a and b, adds b list to the end of the a list.

    // 2)
    type T<'a,'b> =
        | A of 'a
        | B of 'b
        | C of T<'a,'b> * T<'a,'b>
    
    let typEx2 = C (A 1, B true)
    
    // 3)
    let typEx3 = C (A ([]:'a list), B (None:'b option))
    
    // 4)
    // the type typ represents a binary tree structure where leafs are on the left are of type 'a and on the right are of type 'b
    // 
    // f1 finds the amount of levels in the "longest" branch of the typ input. 
    // f2 creates a list with the values of the typ tree, from left to right
    // f3 if b = true then f3 adds the value e as an A to the last leftmost branch. Otherwise if b = false then f3 adds e as an B value on the rightmost branch
    
    let rec f3 e b t =
        match t with
            | C(t1,t2) when b -> C(f3 e b t1, t2)
            | C(t1,t2) -> C(t1, f3 e b t2)
            | _ when b -> C(A e, t)
            | _ -> C(t, B e)

module ex4 = 
    type 'a tree = 
        | Lf
        | Br of 'a * 'a tree * 'a tree
        
    let rec sumTree = function
        | Lf -> 0                                           (* sT1 *)
        | Br(x, t1, t2) -> x + sumTree t1 + sumTree t2      (* sT2 *)

    let rec toList = function
        | Lf -> []                                      (* tL1 *)
        | Br(x, t1, t2) -> x::(toList t1@toList t2)     (* tL2 *)
                
    let rec sumList = function
        | [] -> 0                       (* sL1 *)
        | x::xs -> x + sumList xs;;     (* sL2 *)

    let rec sumListA n = function
        | [] -> n                       (* sLA1 *)
        | x::xs -> sumListA (n+x) xs;;  (* sLA2 *)
