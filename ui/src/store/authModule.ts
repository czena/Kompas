import {Action, getModule, Module, Mutation, VuexModule} from "vuex-module-decorators";
import store from "@/store/index";
import env from "../../environment";
import {LoginResponse} from "@/contract/responses/loginResponse";
import axios from "axios";
import {LoginRequest} from "@/contract/requests/loginRequest";
import {circleModule} from "@/store/circleModule";

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
        let address = env.LoginApi;
        let loginResponse: LoginResponse | null = null;
        await axios.post<LoginResponse>(address, request).then(function(res){
            loginResponse = res.data;
        });
        if (loginResponse != null) {
            let token = (<LoginResponse>loginResponse)?.token;
            if (!token || token == ""){
                this.isAuthenticatedMutation({value: false, token: ""});
            } 
            else {
                this.isAuthenticatedMutation({value: true, token: token});
            }
        }
    }

    @Action({rawError: true})
    public async validateToken(){
        let token = localStorage.getItem("token");
        let address = env.ValidateTokenApi;
        let status = 200;
        await axios.get(address, {
            headers:{
                Authorization: "Bearer " + token
            }
        }).then(function(res){
            status = res.status;
        }).catch(function(error){
            console.log(error);
        });
        if (status == 200){
            await circleModule.getCircles();
            this.isAuthenticatedMutation({value: true, token: token!});
        }
    }
}

export const authModule = getModule(AuthModule);