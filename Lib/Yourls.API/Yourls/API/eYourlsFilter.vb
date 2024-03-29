﻿' NuGardt YOURLS API
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

Namespace Yourls.API
  ''' <summary>
  ''' Filter for getting URLs.
  ''' </summary>
  ''' <remarks></remarks>
  Public Enum eYourlsFilter
    ''' <summary>
    ''' Get the top X of URLS.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumMember(Value := "top")> _
    Top

    ''' <summary>
    ''' Get the bottom X of URLs.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumMember(Value := "bottom")> _
    Bottom

    ''' <summary>
    ''' Get X random URLs.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumMember(Value := "rand")> _
    Rand

    ''' <summary>
    ''' Get the X latests URLs.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumMember(Value := "last")> _
    Last
  End Enum
End Namespace