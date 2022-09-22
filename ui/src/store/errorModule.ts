import {Action, getModule, Module, Mutation, VuexModule} from "vuex-module-decorators";
import store from "@/store/index";
import {AxiosError} from "axios";
import {authModule} from "@/store/authModule";

@Module({store: store, name: 'errorModule' })
export class ErrorModule extends VuexModule {
    private _hasError: boolean = false;
    private _errorCode: number | undefined = undefined;
    
    public get HasError(): boolean{
        return this._hasError;
    }

    public get ErrorCode(): number | undefined{
        return this._errorCode;
    }
    
    @Mutation
    private setErrorMutation(error: {hasError: boolean, errorCode: number | undefined}){
        this._hasError = error.hasError;
        this._errorCode = error.errorCode;
    }
    
    
    @Action({rawError: true})
    public setError(error: AxiosError){
        if (error.response !== undefined){
            this.setErrorMutation({hasError: true, errorCode: error.response.status});
            if (error.response.status == 401){
                authModule.isAuthenticatedMutation({value: false, token: ""})
            }
        }
        else{
            this.setErrorMutation({hasError: true, errorCode: undefined});
        }
    }

    @Action({rawError: true})
    public setCode(code: number){
        this.setErrorMutation({hasError: true, errorCode: code});
    }

    @Action({rawError: true})
    public clear(){
        this.setErrorMutation({hasError: false, errorCode: undefined});
    }
}

export const errorModule = getModule(ErrorModule);