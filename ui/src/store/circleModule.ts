import store from "@/store/index";
import {Action, getModule, Module, Mutation, VuexModule} from "vuex-module-decorators";
import {Circle} from "@/models/circle";
import env from "../../environment";
import axios, {AxiosError, AxiosResponse} from "axios";
import {GetCirclesResponse} from "@/contract/responses/getCirclesResponse";
import {CircleMapper} from "@/contract/mappers/circleMapper";
import {SetDescriptionRequest} from "@/contract/requests/setDescriptionRequest";
import {GetDescriptionResponse} from "@/contract/responses/getDescriptionResponse";
import {GetDescriptionRequest} from "@/contract/requests/getDescriptionRequest";
import {errorModule} from "@/store/errorModule";

@Module({store: store, name: 'circleModule' })
export class CircleModule extends VuexModule {
    private _circles: Circle[] = [];

    public get Circles(): Circle[]{
        return this._circles;
    }
    
    public get CirclesMinPosition(): {x: number, y: number}{
        if (this._circles.length == 0) return {x: 0, y: 0};
        return {x: Math.min(...this._circles.map(x => x.x - 15)), y: Math.min(...this._circles.map(x => x.y - 15))};
    }
    
    @Mutation
    private addCircleMutation(circles: Circle[]){
        this._circles = circles;
    }
    
    @Action({rawError: true})
    public async getCircles(){
        let token = localStorage.getItem("token");
        await axios.get<GetCirclesResponse>(env.GetCirclesApi, {
            headers:{
                Authorization: "Bearer " + token
            }
        }).then(function(res: AxiosResponse<GetCirclesResponse>){
            if(res.status == 200){
                let circles = res.data.circles;
                circleModule.addCircleMutation(circles.map(c => CircleMapper.ToModel(c)));
            }
            else errorModule.setCode(res.status);
        }).catch(function(error: AxiosError){
            errorModule.setError(error);
        });
    }

    @Action({rawError: true})
    public async setDescription(circle: {id: number, newDescription: string}){
        let token = localStorage.getItem("token");
        let request = new SetDescriptionRequest(circle.id, circle.newDescription);

        await axios.post(env.SetDescriptionApi, request, {
            headers:{
                Authorization: "Bearer " + token
            }
        }).catch(function(error: AxiosError){
            errorModule.setError(error);
        });
    }

    @Action({rawError: true})
    public async getDescription(id: number){
        let token = localStorage.getItem("token");
        let request = new GetDescriptionRequest(id);

        return await axios.post(env.SetDescriptionApi, request, {
            headers: {
                Authorization: "Bearer " + token
            }
        }).then(function (res: AxiosResponse<GetDescriptionResponse>) {
            if (res.status == 200) {
                return res.data.description;
            }
            else errorModule.setCode(res.status);
        }).catch(function (error: AxiosError) {
            errorModule.setError(error);
        });
    }
}

export const circleModule = getModule(CircleModule);