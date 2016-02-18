module Assignment03
// *
    // Exercise 3.1
// * 
type 'a BinTree =
        | Leaf
        | Node of 'a * 'a BinTree * 'a BinTree

let intBinTree = Node(43, Node(25, Node(56,Leaf, Leaf), Leaf), Node(562, Leaf, Node(78, Leaf, Leaf)))

let rec preOrder tree =
    match tree with
    | Leaf -> []
    | Node(n,treeL,treeR) ->
        n :: (preOrder treeL) @ (preOrder treeR)

// only hand in this.
let rec inOrder tree =
    match tree with
    | Leaf -> []
    | Node(n,treeL,treeR) ->
        (inOrder treeL) @ (n::(inOrder treeR))

//// Not part of hand in 
//let rec postOrder tree =
//    match tree with
//    | Leaf -> []
//    | Node(n,treeL,treeR) ->
//        ((postOrder treeL) @ (postOrder treeR))@[n]

// *
    // Exercise 3.2
// * 
let rec mapInOrder f tree =
    match tree with
    | Leaf -> Leaf
    | Node(n,treeL, treeR) -> 
        let left = mapInOrder f treeL
        let mid = f n
        let right = mapInOrder f treeR
        Node(n, left, right)

//Since mapPostOrder goes through the tree differently, all functions f which prints or creates side effects, will create a different result than the one from an inOrder mapping
//But the end tree and the individual nodes will still have the same values.


// *
    // Exercise 3.3
// * 
let floatBinTree = Node(43.0,Node(25.0, Node(56.0,Leaf, Leaf), Leaf), Node(562.0, Leaf, Node(78.0, Leaf,Leaf)))

let foldInOrder f a tree = List.foldBack f (inOrder tree ) a

let answer = (foldInOrder (fun n a -> a + n) 0.0 floatBinTree)=764.0

// *
    // Exercise 3.4
// * 
(*
Complete the program skeleton for the interpreter presented on slide 28 in the slide deck from the lecture 5 about finite trees.
Define 5 examples and evaluate them.
The declaration for the abstract syntax for arithmetic expressions follows the grammar (slide 23):
*)
module ex34 = 
    type aExp = (* Arithmetical expressions *)
        | N of int (* numbers *)
        | V of string (* variables *)
        | Add of aExp * aExp (* addition *)
        | Mul of aExp * aExp (* multiplication *)
        | Sub of aExp * aExp (* subtraction *)
  
    let rec A a (s:Map<string,int>) = (* Arithmetical expressions *)
        match a with 
        | N n -> n (* numbers *)
        | V x -> Map.find x s (* variables *)   
        | Add(a1, a2) -> A a1 s + A a2 s (* addition *)
        | Mul(a1, a2) -> A a1 s * A a2 s (* multiplication *)
        | Sub(a1, a2) -> A a1 s - A a2 s (* subtraction *)
    
    type bExp = (* Boolean expressions *)
        | TT (* true *)
        | FF (* false *)
        | Eq of aExp * aExp (* equality *)
        | Lt of aExp * aExp (* less than *)
        | Neg of bExp (* negation *)
        | Con of bExp * bExp (* conjunction *)

    let rec B b s = (* Arithmetical expressions *)
        match b with 
        | TT -> true(* true *)
        | FF -> false(* false *)
        | Eq(a1, a2) -> A a1 s = A a2 s (* equality *)
        | Lt(a1, a2) -> A a1 s < A a2 s (* less than *)
        | Neg(b) -> not(B b s) (* negation *)
        | Con(b1, b2) -> B b1 s && B b2 s (* conjunction *)

    type stm = (* statements *)
        | Ass of string * aExp (* assignment *)
        | Skip
        | Seq of stm * stm (* sequential composition *)
        | ITE of bExp * stm * stm (* if-then-else *)
        | While of bExp * stm (* while *)

    let update x v s = Map.add x v s

    let rec I stm s =
        match stm with
            | Ass(x,a) -> update x (A a s) s
            | Skip -> s
            | Seq(stm1, stm2) -> (I stm1 s) |> I stm2
            | ITE(b,stm1,stm2) -> if B b s then I stm1 s else I stm2 s
            | While(b, stmNew) -> if B b s then I stm (I stmNew s) else s

    //Examples:
    let example1 = I (Seq(Ass("x", N 1),While(Lt(V "x",N 100), Ass("x", Add(V "x",N 2))))) Map.empty
    let example2 = I (Seq(Ass("x", N 2), Ass("x", Mul(V "x", N 4)))) Map.empty
    let example3 = I (Skip) Map.empty
    let example4 = I (ITE(Con(Neg TT, FF),Ass("x", N 1),Ass("x", N -1))) Map.empty
    let example5 = I (Ass("x", Sub(N 2, (Sub (Mul(N 1, N 3), N 2))))) Map.empty
                     

// *
    // Exercise 3.5
// * 
module ex35 = 
    let update x v s = Map.add x v s
    type aExp = (* Arithmetical expressions *)
        | N of int (* numbers *)
        | V of string (* variables *)
        | Add of aExp * aExp (* addition *)
        | Mul of aExp * aExp (* multiplication *)
        | Sub of aExp * aExp (* subtraction *)
  
    let rec A a (s:Map<string,int>) = (* Arithmetical expressions *)
        match a with 
        | N n -> n (* numbers *)
        | V x -> Map.find x s (* variables *)   
        | Add(a1, a2) -> A a1 s + A a2 s (* addition *)
        | Mul(a1, a2) -> A a1 s * A a2 s (* multiplication *)
        | Sub(a1, a2) -> A a1 s - A a2 s (* subtraction *)

    type bExp = (* Boolean expressions *)
        | TT (* true *)
        | FF (* false *)
        | Eq of aExp * aExp (* equality *)
        | Lt of aExp * aExp (* less than *)
        | Neg of bExp (* negation *)
        | Con of bExp * bExp (* conjunction *)

    let rec B b s = (* Arithmetical expressions *)
        match b with 
        | TT -> true(* true *)
        | FF -> false(* false *)
        | Eq(a1, a2) -> A a1 s = A a2 s (* equality *)
        | Lt(a1, a2) -> A a1 s < A a2 s (* less than *)
        | Neg(b) -> not(B b s) (* negation *)
        | Con(b1, b2) -> B b1 s && B b2 s (* conjunction *)

    type stm = (* statements *)
        | Ass of string * aExp (* assignment *)
        | Skip
        | Seq of stm * stm (* sequential composition *)
        | ITE of bExp * stm * stm (* if-then-else *)
        | IT of bExp * stm  (* if-then *)
        | While of bExp * stm (* while *)
        | RU of stm * bExp (* Repeat until *)
    let rec I stm s =
        match stm with
            | Ass(x,a) -> update x (A a s) s
            | Skip -> s
            | Seq(stm1, stm2) -> (I stm1 s) |> I stm2
            | ITE(b,stm1,stm2) -> if B b s then I stm1 s else I stm2 s
            | IT(b,stm1) -> if B b s then I stm1 s else I Skip s
            | While(b, stmNew) -> if B b s then I stm (I stmNew s) else s
            | RU(stmNew, b) -> let newS = I stm s
                               if (B (Neg b) s) then I stmNew newS else newS

// *
    // Exercise 3.6
// *

//    To complete this exercise The A definition needs to be changed such that it returns both a value and a state. 
//    This creates a lot of changes to the system, since both B and I uses A.
//    It would not be possible to just use A and update the state and return the value, since the state would then be thrown away.
//    And one could not just use I since it does not return a value. 
//    A solution could be to use a global state and then create the Inc function in A.


// *
    // Exercise 3.7 : HR exercise 6.2
// * 
module ex37 = 
    type Fexpr = 
        | Const of float
        | X
        | Add of Fexpr * Fexpr
        | Sub of Fexpr * Fexpr
        | Mul of Fexpr * Fexpr
        | Div of Fexpr * Fexpr
        | Sin of Fexpr
        | Cos of Fexpr
        | Log of Fexpr
        | Exp of Fexpr
    
    let rec postfix = function
            | Const c -> string c
            | X -> "x"
            | Add (a1, a2) -> "("+ postfix a1 + ") (" + postfix a2 + ") +" 
            | Sub (a1, a2) -> "("+ postfix a1 + ") (" + postfix a2 + ") -" 
            | Mul (a1, a2) -> "("+ postfix a1 + ") (" + postfix a2 + ") *" 
            | Div (a1, a2) -> "("+ postfix a1 + ") (" + postfix a2 + ") /" 
            | Sin a -> "("+ postfix a + ") Sin" 
            | Cos a -> "("+ postfix a + ") Cos"
            | Log a -> "("+ postfix a + ") Log"
            | Exp a -> "("+ postfix a + ") Exp"

// *
    // Exercise 3.8 : HR exercise 6.8
// * 
module ex38 = 
    type Instruction = 
    | ADD 
    | SUB 
    | MULT 
    | DIV 
    | SIN
    | COS 
    | LOG 
    | EXP 
    | PUSH of float

    type stack = float list
    exception InstructionOnEmptyList
    exception InvalidInput

    let intpInstr stack instr = 
        match (stack, instr) with
            | (_, PUSH c) -> c::stack
            | ([], _) -> raise InstructionOnEmptyList
            | (x::xs, SIN) -> (System.Math.Sin x)::xs
            | (x::xs, COS) -> (System.Math.Cos x)::xs
            | (x::xs, LOG) -> (System.Math.Log x)::xs
            | (x::xs, EXP) -> (System.Math.Exp x)::xs
            | (x::xk::xs, ADD) -> (xk+x)::xs        
            | (x::xk::xs, SUB) -> (xk-x)::xs
            | (x::xk::xs, MULT) -> (xk*x)::xs
            | (x::xk::xs, DIV) -> (xk/x)::xs
            | (_,_) -> raise InvalidInput

    type Program = Instruction list
    
    let intpProgram (program:Program) = let x = (List.fold (fun x xs -> intpInstr x xs) [] program) 
                                        if x.Length=1 then x.Head else raise InvalidInput

    type Fexpr = 
        | Const of float
        | X
        | Add of Fexpr * Fexpr
        | Sub of Fexpr * Fexpr
        | Mul of Fexpr * Fexpr
        | Div of Fexpr * Fexpr
        | Sin of Fexpr
        | Cos of Fexpr
        | Log of Fexpr
        | Exp of Fexpr

    let rec trans (fExpr,input) list = match (fExpr) with
                                       | (Const c) -> (PUSH c)::list
                                       | (X)       -> (PUSH input)::list
                                       | (Sin newExp) -> trans (newExp, input) (SIN::list)
                                       | (Cos newExp) -> trans (newExp, input) (COS::list)
                                       | (Log newExp) -> trans (newExp, input) (LOG::list)
                                       | (Exp newExp) -> trans (newExp, input) (EXP::list)
                                       | (Add (newExp1,newExp2)) -> trans (newExp1, input) (trans (newExp2, input) (ADD::list))
                                       | (Sub (newExp1,newExp2)) -> trans (newExp1, input) (trans (newExp2, input) (SUB::list))
                                       | (Mul (newExp1,newExp2)) -> trans (newExp1, input) (trans (newExp2, input) (MULT::list))
                                       | (Div (newExp1,newExp2)) -> trans (newExp1, input) (trans (newExp2, input) (DIV::list))
// *  
    // Exercise 3.9 : HR exercise 7.2
// *
// See the two other files.