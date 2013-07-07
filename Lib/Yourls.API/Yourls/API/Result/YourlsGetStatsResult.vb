' NuGardt YOURLS API
' Copyright (C) 2013 NuGardt Software
' http://www.nugardt.com
'
' This program is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program.  If not, see <http://www.gnu.org/licenses/>.
'
Imports System.Runtime.Serialization

Namespace Yourls.API.Result
  ''' <summary>
  ''' YOURLS API response.
  ''' </summary>
  ''' <remarks></remarks>
    <DataContract()>
  Public Class YourlsGetStatsResult
    Inherits YourlsResult

    'SUCESS
    '{"links":
    '{
    '"link_1":
    '{
    '"shorturl":"http:\/\/ls.nugardt.com\/2go8s",
    '"url":"http:\/\/www.nugardt.com",
    '"title":"Home | NuGardt Software",
    '"timestamp":"2013-07-06 05:50:40",
    '"ip":"92.226.234.122",
    '"clicks":"1"
    '},
    '"link_2":
    '{
    '"shorturl":"http:\/\/ls.nugardt.com\/95tyl",
    '"url":"https:\/\/twitter.com\/NuGardt",
    '"title":"NuGardt Software (NuGardt) auf Twitter",
    '"timestamp":"2013-07-06 05:52:52",
    '"ip":"92.226.234.122",
    '"clicks":"1"
    '},
    '"link_3":
    '{
    '"shorturl":"http:\/\/ls.nugardt.com\/5kzkb",
    '"url":"http:\/\/jira.nugardt.com:9080",
    '"title":"http:\/\/jira.nugardt.com:9080",
    '"timestamp":"2013-07-06 05:52:02",
    '"ip":"92.226.234.122",
    '"clicks":"0"
    '},
    '"link_4":
    '{
    '"shorturl":"http:\/\/ls.nugardt.com\/f0yk0",
    '"url":"https:\/\/jira.nugardt.com:9443",
    '"title":"https:\/\/jira.nugardt.com:9443",
    '"timestamp":"2013-07-06 05:52:19",
    '"ip":"92.226.234.122",
    '"clicks":"0"
    '}
    ',"link_5":
    '{
    '"shorturl":"http:\/\/ls.nugardt.com\/o1628",
    '"url":"http:\/\/www.seismovision.net",
    '"title":"Seismovision - The Demo Player",
    '"timestamp":"2013-07-06 05:52:29",
    '"ip":"92.226.234.122",
    '"clicks":"0"
    '}
    '},
    '"stats":
    '{
    '"total_links":"5",
    '"total_clicks":"2"
    '}
    ',"statusCode":200,
    '"message":"success"
    '}

    'ERROR
    '{
    '"message":"Invalid username or password",
    '"errorCode":403,
    '"callback":""
    '}

    <DataMember(Name := "links")>
    Public Links As Links

    <DataMember(Name := "stats")>
    Public Stats As DatabaseStats
  End Class
End Namespace