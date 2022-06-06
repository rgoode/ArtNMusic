import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AlbumByArtist } from './models/AlbumByArtist';
import { CoverArtArchive } from './models/CoverArtArchive';
import { Album } from './models/Album';

@Injectable({
  providedIn: 'root'
})
export class CoverartarchiveService {

  album: Album | undefined;

  constructor(private httpClient: HttpClient) { }
  baseUrl = "http://localhost:5265/release";

  

  getAlbumCoverArt(mbid: string){
    console.log(mbid);
    return this.httpClient.get<CoverArtArchive>(`${this.baseUrl}/${mbid}`);
  }


}