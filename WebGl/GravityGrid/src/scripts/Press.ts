export class Press {
    public current: { x:number, y:number };
    public old: { x:number, y:number };
    public delta: { x:number, y:number };
    public power: number = 0;
    public isDead: boolean = false;
    public isMouse: boolean = false;

    public readonly id: number;

    constructor(id: number) {
        this.id = id;
    }
}