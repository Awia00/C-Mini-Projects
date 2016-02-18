module Examn_jun2014

module ex1 =
    type OrderedList<'a when 'a : equality> =
            {front: 'a list;
             rear: 'a list}
    let ex = {front = ['x']; rear = ['z';'y']}

    (*
    1.1
    There is a total of 4 combinations.
    *)
    let ol1 = {front = ["Hans"; "Brian"; "Gudrun"]; rear = []}
    let ol2 = {front = ["Hans"; "Brian"]; rear = ["Gudrun"]}
    let ol3 = {front = ["Hans"]; rear = ["Gudrun"; "Brian"]}
    let ol4 = {front = []; rear = ["Gudrun"; "Brian"; "Hans"]}

    
    (*
    1.2
    *)
    let ol5 = {front = []; rear = []}

    let canonical (o:OrderedList<'a>) : OrderedList<'a> = {front = o.front@List.rev o.rear ; rear = []}
    let toList (o:OrderedList<'a>) : List<'a> = o.front@  List.rev o.rear

    (*
    1.3
    *)
    let newOl<'a when 'a : equality> = {front=List.empty<'a> ; rear =List.empty<'a>}
    let isEmpty (o:OrderedList<'a>) : bool = o=newOl

    (*
    1.4
    *)
    let addFront (w:'a) (o:OrderedList<'a>) : OrderedList<'a> = {front = w::o.front ; rear = o.rear}
    let removeFront (o:OrderedList<'a>) : ('a * OrderedList<'a>) = 
        match o with
        | {front=[]; rear=_}        -> failwith "bla"
        | {front=x::[]; rear=_}     -> (x,newOl)
        | {front=x::xs; rear=_}     -> (x, {front = xs ; rear=o.rear})
    let peekFront (o:OrderedList<'a>) : 'a = 
        match o with
        | {front=[];    rear=_}     -> failwith "bla"
        | {front=x::[]; rear=_}     -> x
        | {front=x::xs; rear=_}     -> x
    
    (*
    1.5
    *)
    let append (o1:OrderedList<'a>) (o2:OrderedList<'a>) : OrderedList<'a> = {front = toList o1 ; rear = toList o2}

    (*
    1.6
    *)
    let map f (o:OrderedList<'a>) : OrderedList<'b> = {front=(List.map f o.front) ; rear=(List.map f o.rear)}
    
    (*
    1.7
    *)
    let fold f (s:'State) (o:OrderedList<'T>) : 'State = List.fold f s (toList o)
    
    (*
    1.8
    *)
    let multiplicity (o:OrderedList<'a>) : Map<'a,int> =
        let rec inner (innerO:OrderedList<'a>) (accMap: Map<'a,int>) : Map<'a,int> =
            let addOrIncrement (element: 'a) (innerMap: Map<'a,int>) : Map<'a,int> = if (innerMap.ContainsKey element) then innerMap.Add (element, (Option.get (innerMap.TryFind element) + 1)) else innerMap.Add (element, 1)
            match innerO with
            | {front=[];    rear=_} -> accMap
            | {front=x::[]; rear=_} -> addOrIncrement x accMap
            | {front=x::xs; rear=_} -> inner {front = xs ; rear = []} (addOrIncrement x accMap)
        inner (canonical o) Map.empty

module ex2 =
    let rec f i = function
       | [] -> [i]
       | x::xs -> i+x :: f (i+1) xs
    
    (* Question 2.1
    1)
        f computes the list of each element in the incoming list + i + their index in the list. Furthermore a last element is added which is equal to i + the size of the input list.
    
    2)
        The result of f can never be the empty list since all cases are covered by the pattern match, and an empty input list results in a list with atleast one element i.

    3)
        The function cannot go into an infinite loop. Since the input list must have a given size, and the function is called recoursivily with one element less than the input, 
        and the function stops getting called recoursivily when the list is empty, it cannot go into an infinite loop
    *)

    // Question 2.2
    let fA i list = 
        let rec innerRec iAcc acc = function
            | [] -> [iAcc]
            | x::xs -> innerRec (iAcc+1) (iAcc+x::acc) xs
        innerRec i List.empty list

    // Question 2.3 - NOT DONE
    let rec fC i = function
       | [] -> [i]
       | x::xs -> i+x :: fC (i+1) xs

module ex3 =
    let myFinSeq n M = Seq.map (fun m -> n+n*m) [0..M]
    // Question 3.1
    (*
        myFinSeq creates a finite sequence with length m+1 (minimum empty sequence), where the elements are equal to n+n*index.
        if n is negative, then the rest of the elements will be negative. All the elements will be in the same multiplication table as n.
    *)

    // Question 3.2
    let mySeq n = Seq.initInfinite (fun i -> n+n*i) 

    // Question 3.3 har en fejl
//    let multTable N M : seq<(int*int*int)> = 
//        let doubleSeq = Seq.map (fun m -> Seq.map (fun n -> n,m,n*m) [0..N]) [0..M]
//        let rec inner inSeq (acc:seq<(int*int*int)>) = 
//            if (Seq.isEmpty inSeq)
//            then acc
//            else inner (Seq.skip 1 inSeq) (Seq.append acc (Seq.head inSeq))
//        inner doubleSeq Seq.empty
    
    let multTable N M : seq<(int*int*int)> = Seq.concat (Seq.map (fun m -> Seq.map (fun n -> n,m,n*m) [0..N]) [0..M])

    // Question 3.4 
    let ppMultTable (N:int) (M:int) : seq<string> = Seq.map (fun ((n,m,x):(int*int*int)) -> string n + " * " + string m + " is "+ string x) (multTable N M)

module MousePlot = 
    type opr = 
        | MovePenUp
        | MovePenDown
        | TurnEast
        | TurnWest
        | TurnNorth
        | TurnSouth
        | Step
    
    type plot = 
        | Opr of opr
        | Seq of plot * plot

    let side = Seq(Opr MovePenDown, Seq(Opr Step, Seq(Opr Step, Opr Step)))
    let rect = Seq(
                Seq(Opr TurnEast, side), 
                Seq(Opr TurnNorth, 
                 Seq(side, 
                  Seq(Opr TurnWest, 
                   Seq(side, 
                    Seq(Opr TurnSouth, side))))))
    // Question 4.1
    let ppOpr (o:opr): string = 
        match o with
        | MovePenUp     -> "MovePenUp"
        | MovePenDown   -> "MovePenDown"
        | TurnEast      -> "TurnEast"
        | TurnWest      -> "TurnWest"
        | TurnNorth     -> "TurnNorth"
        | TurnSouth     -> "TurnSouth"
        | Step          -> "Step"
    let rec ppOprPlot (p:plot) : string = 
        match p with
        | Opr o               -> ppOpr o
        | Seq (plot1, plot2)  -> ppOprPlot plot1 + " => " + ppOprPlot plot2

    // Question 4.2
    type dir = 
        | North
        | South
        | West
        | East
    type pen = 
        | PenUp
        | PenDown
    type coord = int * int
    type state = coord * dir * pen

    let initialState = ((0,0),East,PenUp)

    let goStep (((x,y),dir,pen):state) : state = 
        match dir with
        | North -> ((x,y+1), dir, pen)
        | South -> ((x,y-1), dir, pen)
        | West  -> ((x-1,y), dir, pen)
        | East  -> ((x+1,y), dir, pen)

    let performOpr (o:opr) ((coord,dir,pen):state) : state = 
        match o with
        | MovePenUp     -> (coord,dir,PenUp)
        | MovePenDown   -> (coord,dir,PenDown)
        | TurnEast      -> (coord,East,pen)
        | TurnWest      -> (coord,West,pen)
        | TurnNorth     -> (coord,North,pen)
        | TurnSouth     -> (coord,South,pen)
        | Step          -> goStep (coord,dir,pen)

    let addDot ((coord,dir,pen):state) (c:list<coord>) (o:opr) :((list<coord>)*(state)) = 
        let (newCoord,newDir,newPen) = performOpr o (coord,dir,pen)
        if (newPen=PenDown)
        then (newCoord::c,(newCoord,newDir,newPen)) 
        else (c,(newCoord,newDir,newPen)) 

    let (coords1,s1) = addDot initialState [] MovePenDown
    let (coords2,s2) = addDot s1 coords1 Step

    let dotCoords (p:plot) : list<coord> =
        let rec inner (pInner:plot) ((acc,s):list<coord>*state) =
            match pInner with
            | Opr o               -> addDot s acc o
            | Seq (plot1, plot2)  -> inner plot1 (acc,s) |> inner plot2
        let coordFinal ((list,s):list<coord>*state) = list
        coordFinal (inner p ([],initialState))
    let uniqueDotCoords (p:plot) : (Set<coord>) = Set.ofList (dotCoords p)
 
    type plot with
        static member (+) (p1, p2) = Seq (p1, p2)
    let side2 = Opr MovePenDown + Opr Step + Opr Step + Opr Step
    let rect2 = Opr TurnEast + side2 + Opr TurnNorth + side2 + Opr TurnWest + side2 + Opr TurnSouth + side2