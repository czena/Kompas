export class Circle{
    public id: number;
    public description: string;
    public x: number;
    public y: number
    
    constructor(id: number, description: string, x: number, y: number) {
        this.id = id;
        this.description = description;
        this.x = x;
        this.y = y;
    }
}