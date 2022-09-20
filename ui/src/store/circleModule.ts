import store from "@/store/index";
import {getModule, Mutation, VuexModule, Module, Action} from "vuex-module-decorators";
import {Circle} from "@/models/circle";
import env from "../../environment";
import axios from "axios";
import {GetCirclesResponse} from "@/contract/responses/getCirclesResponse";
import {plainToInstance} from "class-transformer";
import {CircleDTO} from "@/contract/dtos/circleDTO";
import {CircleMapper} from "@/contract/mappers/circleMapper";

@Module({store: store, name: 'circleModule' })
export class CircleModule extends VuexModule {
    private _circles: Circle[] = [];
    private _editableCircle?: Circle;

    public get Circles(): Circle[]{
        return this._circles;
    }

    public get EditableCircle(): Circle | undefined{
        return this._editableCircle;
    }

    @Mutation
    private addCircleMutation(circles: Circle[]){
        this._editableCircle = undefined;
        this._circles = circles;
    }

    @Mutation
    public circleEditMutation(circle: Circle){
        this._editableCircle = circle;
    }

    @Mutation
    public saveEditableCircleDescriptionMutation(description: string){
        if (!this._editableCircle) return;
        this._editableCircle!.description = description;
        this._editableCircle = undefined;
    }

    @Action({rawError: true})
    public async GetCircles(){
        let token = localStorage.getItem("token");
        let address = env.GetCirclesApi;
        let status = 200;
        let response: GetCirclesResponse | null = null;
        await axios.get<GetCirclesResponse>(address, {
            headers:{
                Authorization: "Bearer " + token
            }
        }).then(function(res){
            status = res.status;
            response = res.data;
        });
        if (status == 200 && response != null){
            let circles = (<GetCirclesResponse>response).circles;
            this.addCircleMutation(circles.map(c => CircleMapper.ToModel(c)))
        }
    }
    
}

export const circleModule = getModule(CircleModule);