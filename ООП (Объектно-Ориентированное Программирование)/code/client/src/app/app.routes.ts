import { RouterModule, Routes } from '@angular/router';
import { AuthLayoutComponent } from './shared/layouts/auth-layout/auth-layout.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { SiteLayoutComponent } from './shared/layouts/site-layout/site-layout.component';
import { NgModule } from '@angular/core';
import { RegisterPageComponent } from './register-page/register-page.component';

export const routes: Routes = [
    {path: '', component: AuthLayoutComponent, children: [
        {path: 'login', component: LoginPageComponent},
        {path: 'register', component: RegisterPageComponent}
    ]},
    {path: '', component: SiteLayoutComponent, children: [

    ]},
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes)
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule {

}