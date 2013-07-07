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
  Public Class YourlsGetDatabaseStatsResult
    Inherits YourlsResult

    'SUCESS
    '{
    '"db-stats":
    '{
    '"total_links":"5",
    '"total_clicks":"2"
    '},
    '"statusCode":200,
    '"message":"success"
    '}

    'ERROR
    '{
    '"message":"Invalid username or password",
    '"errorCode":403,
    '"callback":""
    '}

    <DataMember(Name := "db-stats")>
    Public Stats As DatabaseStats
  End Class
End Namespace