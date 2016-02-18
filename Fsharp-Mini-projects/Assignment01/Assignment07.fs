module Assignment07

// *
    // Exercise 1
// *
// SlowFibo (0..42) non async
// Real: 00:00:06.034, CPU: 00:00:06.046, GC gen0: 0, gen1: 0, gen2: 0
// SlowFibo (0..42) async 
// Real: 00:00:03.564, CPU: 00:00:09.000, GC gen0: 0, gen1: 0, gen2: 0
// So the non aysnc is in this case almost twice as slow, but the actual time spent on the cores are actually almost 50% more when using async.

// *
    // Exercise 2
// *
// Runing Array.init 200000 factors;; using the provided code gives the following data
// Real: 00:00:11.336, CPU: 00:00:11.515, GC gen0: 5, gen1: 2, gen2: 1
// Running let factors200000 = Array.Parallel.init 200000 factors gives the following
// Real: 00:00:04.941, CPU: 00:00:19.203, GC gen0: 4, gen1: 2, gen2: 0
// here we see that the async function is more than twice as fast, but the total CPU core computation time is also a bit under twice as long.

// *
    // Exercise 3
// *
module ex3 =
    let isPrime n =
        let rec testDiv a = a*a > n || n % a <> 0 && testDiv (a+1)
        n>=2 && testDiv 2
    
    let factors n =
        let rec factorsIn d m =
            if m <= 1 then []
            else if m % d = 0 then d :: factorsIn d (m/d) else factorsIn (d+1) m
        factorsIn 2 n
    
    let random n =
        let generator = new System.Random ()
        fun () -> generator.Next n
    
    let r10000 = random 10000 // 150x faster than creating a new System.Random
    
    let rec ntimes (f : unit -> 'a) n =
        if n=0 then () else (ignore (f ()); ntimes f (n-1))
        
    let bigArray = Array.init 500000 (fun _ -> r10000 ())

    let factors200000 = Array.Parallel.init 200000 factors

    let histogram = Array.init 200000 (fun i -> 0)
    let incr i = histogram.[i] <- histogram.[i] + 1
    Array.iter (fun fs -> List.iter incr fs) factors200000

    // Answer:
    let incrGood (accMap: Map<int,int>) i = 
        let element = Map.tryFind i accMap
        if (element.IsSome) 
            then
                Map.add i (element.Value + 1) accMap
            else
                Map.add i 1 accMap
    let histogramWithoutSideEffects = Array.fold (fun x fs -> List.fold incrGood x fs) Map.empty factors200000

    // this solution only provides a map of primes and their occurences not all numbers and how they are a prime factor 0 times.

// *
    // Exercise 4
// *
module ex4 =
    let isPrime n =
        let rec testDiv a = a*a > n || n % a <> 0 && testDiv (a+1)
        n>=2 && testDiv 2
    
    let rec primeNumbers n =
        let numbers = Array.init (n/2-1) (fun x -> x*2+1)
        Array.length (Array.Parallel.choose (fun x -> (if (isPrime x) then Some x else None)) numbers) + 1
    // 173 characters
    // Real: 00:00:02.828, CPU: 00:00:11.062, GC gen0: 5, gen1: 1, gen2: 1
    // 4 cores, 1.8 GHz