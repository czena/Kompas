import store from "@/store/index";
import {getModule, Mutation, VuexModule, Module} from "vuex-module-decorators";
import {Circle} from "@/models/Circle";

@Module({store: store, name: 'circleModule' })
export class CircleModule extends VuexModule {
    private circles: Circle[] = [];
    private editableCircle?: Circle;

    public get Circles(): Circle[]{
        return this.circles;
    }

    public get EditableCircle(): Circle | undefined{
        return this.editableCircle;
    }

    @Mutation
    public addCircleMutation(){
        this.circles.push(new Circle(1, "H", 50, 50));
        this.circles.push(new Circle(2, "H", 150, 150));
        this.circles.push(new Circle(3, "H", 15000, 150000));
    }

    @Mutation
    public circleEditMutation(circle: Circle){
        this.editableCircle = circle;
    }

    @Mutation
    public saveEditableCircleDescriptionMutation(description: string){
        if (!this.editableCircle) return;
        this.editableCircle!.description = description;
        this.editableCircle = undefined;
    }
    
}

export const circleModule = getModule(CircleModule);