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
Imports System.Text

Namespace Yourls.API.Result.Element
  ''' <summary>
  ''' Database Stats.
  ''' </summary>
  ''' <remarks></remarks>
  <DataContract()>
  Public Class YourlsDatabaseStats
    ''' <summary>
    ''' Returns or sets the total number of link on the YOURLS server.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "total_links")>
    Public TotalLinks As Int64

    ''' <summary>
    ''' Returns or sets the total number of clicks on the YOURLS server.
    ''' </summary>
    ''' <remarks></remarks>
    <DataMember(Name := "total_clicks")>
    Public TotalClicks As Int64

    Public Overrides Function ToString() As String
      Dim SB As New StringBuilder

      With SB
        Call .AppendFormat("Total Links: {0}{1}", Me.TotalLinks.ToString(), vbCrLf)
        Call .AppendFormat("Total Clicks: {0}{1}", Me.TotalClicks.ToString(), vbCrLf)
      End With

      Return SB.ToString()
    End Function
  End Class
End Namespace