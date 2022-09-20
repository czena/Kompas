import {Circle} from "@/models/circle";
import {CircleDTO} from "@/contract/dtos/circleDTO";

export class CircleMapper{
    public static ToModel(circle: CircleDTO): Circle{
        return new Circle(circle.id, circle.description, circle.x, circle.y);
    }
}