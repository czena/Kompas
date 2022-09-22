import {Action, getModule, Module, Mutation, VuexModule} from "vuex-module-decorators";
import store from "@/store/index";
import env from "../../environment";
import {LoginResponse} from "@/contract/responses/loginResponse";
import axios, {AxiosError, AxiosResponse} from "axios";
import {LoginRequest} from "@/contract/requests/loginRequest";
import {circleModule} from "@/store/circleModule";
import {errorModule} from "@/store/errorModule";

@Module({store: store, name: 'authModule' })
export class AuthModule extends VuexModule {
    private _isAuthenticated: boolean = false;
    
    public get IsAuthenticated(): boolean{
        return this._isAuthenticated;
    }

    @Mutation
    public isAuthenticatedMutation(isAuthenticated: {value: boolean, token: string}){
        localStorage.setItem("token", isAuthenticated.token);
        this._isAuthenticated = isAuthenticated.value;
    }
    
    @Action({rawError: true})
    public async login(user: {login: string, password: string}){
        let request = new LoginRequest(user.login, user.password);
        await axios.post<LoginResponse>(env.LoginApi, request).
        then(function(res: AxiosResponse<LoginResponse>){
            if (res.status == 200){
                if (res?.data) {
                    let token = res.data.token;
                    if (!token || token == ""){
                        authModule.isAuthenticatedMutation({value: false, token: ""});
                    }
                    else {
                        authModule.isAuthenticatedMutation({value: true, token: token});
                    }
                }
            }
            else errorModule.setCode(res.status);
        }).catch(function(error){
            errorModule.setError(error);
        });
    }

    @Action({rawError: true})
    public async validateToken(){
        let token = localStorage.getItem("token");
        await axios.get(env.ValidateTokenApi, {
            headers:{
                Authorization: "Bearer " + token
            }
        }).then(async function(res){
            if (res.status == 200){
                await circleModule.getCircles();
                authModule.isAuthenticatedMutation({value: true, token: token!});
            }
            else errorModule.setCode(res.status);
        }).catch(function(error: AxiosError){
        });
    }
}

export const authModule = getModule(AuthModule);