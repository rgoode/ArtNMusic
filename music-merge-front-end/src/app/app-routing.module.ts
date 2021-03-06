import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { NeuralStyleUploadComponent } from "./neural-style-upload/neural-style-upload.component";
import { LoginComponent } from "./login/login.component";
import { GeneratedArtPieceComponent } from "./generated-art-piece/generated-art-piece.component";
import { AlbumListViewComponent } from "./album-list-view/album-list-view.component";
import { ArtistSelectComponent } from "./artist-select/artist-select.component";

const routes: Routes = [

    {
        path: "home",
        component: ArtistSelectComponent
    },
    {
        path: "login",
        component: LoginComponent
    },
    {
        path: "neural-style-upload",
        component: NeuralStyleUploadComponent

    },
    {
        path: "generated-art-piece",
        component: GeneratedArtPieceComponent
    },
    {
        path: "*/",
        component: HomeComponent
    },


];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule{}