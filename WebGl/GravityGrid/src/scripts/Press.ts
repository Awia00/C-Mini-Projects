export class Press {
    public current: { x:number, y:number } = {x:0,y:0};
    public old: { x:number, y:number } = {x:0,y:0};
    public delta: { x:number, y:number };
    public power: number = 3;
    public isDead: boolean = false; 

    public readonly id: number;

    constructor(id: number) {
        this.id = id;
    }
}