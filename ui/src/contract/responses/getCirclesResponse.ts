import {CircleDTO} from "@/contract/dtos/circleDTO";

export class GetCirclesResponse{
    public circles: CircleDTO[];
    
    constructor(circles: CircleDTO[]) {
        this.circles = circles;
    }
}