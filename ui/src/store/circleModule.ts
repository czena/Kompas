import store from "@/store/index";
import {getModule, Mutation, VuexModule, Module, Action} from "vuex-module-decorators";
import {Circle} from "@/models/circle";
import env from "../../environment";
import axios from "axios";
import {GetCirclesResponse} from "@/contract/responses/getCirclesResponse";
import {CircleMapper} from "@/contract/mappers/circleMapper";
import {SetDescriptionRequest} from "@/contract/requests/setDescriptionRequest";

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
    private saveEditableCircleDescriptionMutation(description: string){
        if (!this._editableCircle) return;
        this._editableCircle.description = description;
        this._editableCircle = undefined;
    }

    @Action({rawError: true})
    public async getCircles(){
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

    @Action({rawError: true})
    public async setDescription(circle: {id: number, newDescription: string}){
        let token = localStorage.getItem("token");
        let address = env.SetDescriptionApi;
        let status = 200;
        let request = new SetDescriptionRequest(circle.id, circle.newDescription);
        try{
            await axios.post(address, request, {
                headers:{
                    Authorization: "Bearer " + token
                }
            }).then(function(res){
                status = res.status;
            })
        }
        catch (error) {
            console.log(error);
        }

        if (status == 200){
            this.saveEditableCircleDescriptionMutation(circle.newDescription);
        }
    }
}

export const circleModule = getModule(CircleModule);