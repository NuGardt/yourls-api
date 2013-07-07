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
Namespace Yourls.API
  Public Class YourlsAuthentication
    Implements IYourlsAuthentication

    Private ReadOnly UnixBaseTime As DateTime = New DateTime(1970, 1, 1, 0, 0, 0)

    Private m_Username As String
    Private m_Password As String
    Private m_Signature As String
    Private m_UnixTimestamp As Nullable(Of Int64)

    ''' <summary>
    ''' Constructor.
    ''' </summary>
    ''' <param name="Username">Your YOURLS username.</param>
    ''' <param name="Password">Your YOURLS password.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Username As String,
                   ByVal Password As String)
      Me.m_Username = Username
      Me.m_Password = Password
      Me.m_Signature = Nothing
      Me.Timestamp = Nothing
    End Sub

    ''' <summary>
    ''' Constructor.
    ''' </summary>
    ''' <param name="Signature">Your YOURLS signature.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Signature As String,
                   Optional ByVal Timestamp As Nullable(Of DateTime) = Nothing)
      Me.m_Username = Nothing
      Me.m_Password = Nothing
      Me.m_Signature = Signature
      Me.Timestamp = Timestamp
    End Sub

    Public Property Username As String Implements IYourlsAuthentication.Username
      Get
        Return Me.m_Username
      End Get
      Set(ByVal Value As String)
        Me.m_Username = Value
      End Set
    End Property

    Public Property Password As String Implements IYourlsAuthentication.Password
      Get
        Return Me.m_Password
      End Get
      Set(ByVal Value As String)
        Me.m_Password = Value
      End Set
    End Property

    Public Property Signature As String Implements IYourlsAuthentication.Signature
      Get
        Return Me.m_Signature
      End Get
      Set(ByVal Value As String)
        Me.m_Signature = Signature
      End Set
    End Property

    Public Property Timestamp As Nullable(Of DateTime)
      Get
        If Me.m_UnixTimestamp.HasValue Then
          Return UnixBaseTime.AddSeconds(Me.m_UnixTimestamp.Value)
        Else
          Return Nothing
        End If
      End Get
      Set(ByVal Value As Nullable(Of DateTime))
        If Value.HasValue Then
          Me.m_UnixTimestamp = Convert.ToInt64((Value.Value - UnixBaseTime).TotalSeconds)
        Else
          Me.m_UnixTimestamp = Nothing
        End If
      End Set
    End Property

    Private Property UnixTimestamp As Nullable(Of Int64) Implements IYourlsAuthentication.UnixTimestamp
      Get
        Return Me.m_UnixTimestamp
      End Get
      Set(ByVal Value As Nullable(Of Int64))
        Me.m_UnixTimestamp = Value
      End Set
    End Property
  End Class
End Namespace