﻿namespace GeKtvi.Toolkit.Win32Kit
{
    public enum WinUserEventType
    {
        ///<summary>    Диапазон значений констант WinEvent, заданных альянсом по взаимодействию со специальными возможностями (AIA) для использования в отрасли. Дополнительные сведения см. в разделе Выделение идентификаторов WinEvent.              </summary>                                                </summary>
        EVENT_AIA_START = 0xAFFF,
        EVENT_AIA_END = 0xA000,
        EVENT_MIN = 0x00000001,
        ///<summary>	Наименьшие и самые высокие возможные значения событий.                               </summary>
        EVENT_MAX = 0x7FFFFFFF,
        ///<summary>	Свойство KeyboardShortcut объекта изменилось. Серверные приложения отправляют данное событие для объектов со специальными возможностями.                     </summary>
        EVENT_OBJECT_ACCELERATORCHANGE = 0x8012,
        ///<summary>	Отправляется при маскировке окна. Скрытое окно по-прежнему существует, но невидимо для пользователя.                                                         </summary>
        EVENT_OBJECT_CLOAKED = 0x8017,
        ///<summary>	Прокрутка объекта окна завершена. В отличие от EVENT_SYSTEM_SCROLLEND, это событие связано с окном прокрутки. Независимо от того, является ли прокрутка горизонтальной или вертикальной, это событие должно отправляться при каждом завершении действия прокрутки.                                           </summary>
        EVENT_OBJECT_CONTENTSCROLLED = 0x8015,
        ///<summary>	Параметр hwnd функции обратного вызова WinEventProc описывает окно прокрутки; параметр idObjectOBJID_CLIENT, а параметр idChild — CHILDID_SELF.              </summary>
        ///<summary>	Объект был создан. Система отправляет это событие для следующих элементов пользовательского интерфейса: курсор, заголовок, элемент управления представлением списка, элемент управлениявкладкой, элемент управленияпанелью инструментов, элемент управления представлением в виде дерева и объект окна . Серверные приложения отправляют данное событие для объектов со специальными возможностями.                                                          </summary>
        EVENT_OBJECT_CREATE = 0x8000,
        ///<summary>	Перед отправкой события для родительского объекта серверы должны отправить его для всех дочерних объектов объекта. Серверы должны убедиться, что все дочерние объекты полностью созданы и готовы принять вызовы IAccess от клиентов, прежде чем родительский объект отправит это событие.                    </summary>
        ///<summary>	Так как родительский объект создается после его дочерних объектов, клиенты должны убедиться, что родительский объект объекта создан перед вызовом IAccessible::get_accParent, особенно если используются функции-перехватчики в контексте.                                                                   </summary>
        ///<summary>	Свойство DefaultAction объекта изменилось. Система отправляет данное событие для диалоговых окон. Серверные приложения отправляют данное событие для объектов со специальными возможностями.                                         </summary>
        EVENT_OBJECT_DEFACTIONCHANGE = 0x8011,
        ///<summary>	Свойство Description объекта изменилось. Серверные приложения отправляют данное событие для объектов со специальными возможностями.                          </summary>
        EVENT_OBJECT_DESCRIPTIONCHANGE = 0x800D,
        ///<summary>	Объект был удален. Система отправляет это событие для следующих элементов пользовательского интерфейса: курсор, заголовок, элемент управления представлением списка, элемент управления вкладкой, элемент управления панелью инструментов, элемент управления представлением в виде дерева и объект окна. Серверные приложения отправляют данное событие для объектов со специальными возможностями.                                                         </summary>
        EVENT_OBJECT_DESTROY = 0x8001,
        ///<summary>	Клиенты предполагают, что все дочерние элементы объекта уничтожаются, когда родительский объект отправляет это событие.                                      </summary>
        ///<summary>	После получения этого события клиенты не вызывают свойства или методы IAccess объекта. Однако указатель интерфейса должен оставаться действительным до тех пор, пока на него есть счетчик ссылок (из-за правил COM), но элемент пользовательского интерфейса может больше не присутствовать. Дальнейшие вызовы указателя интерфейса могут возвращать ошибки сбоя; Чтобы предотвратить это, серверы создают прокси-объекты и отслеживают их срок службы.      </summary>
        ///<summary>	Пользователь начал перетаскивать элемент. Параметры hwnd, idObject и idChild функции обратного вызова WinEventProc определяют перетаскиваемый объект.        </summary>
        EVENT_OBJECT_DRAGSTART = 0x8021,
        ///<summary>	Пользователь завершил операцию перетаскивания перед удалением перетаскиваемого элемента на целевой объект перетаскивания. Параметры hwnd, idObject и idChild функции обратного вызова WinEventProc определяют перетаскиваемый объект.</summary>
        EVENT_OBJECT_DRAGCANCEL = 0x8022,
        ///<summary>	Пользователь удалял элемент в целевом объекте удаления. Параметры hwnd, idObject и idChild функции обратного вызова WinEventProc определяют перетаскиваемый объект.                                                                  </summary>
        EVENT_OBJECT_DRAGCOMPLETE = 0x8023,
        ///<summary>	Пользователь перетащил элемент в границу целевого объекта перетаскивания. Параметры hwnd, idObject и idChild функции обратного вызова WinEventProc определяют целевой объект удаления.                                               </summary>
        EVENT_OBJECT_DRAGENTER = 0x8024,
        ///<summary>	Пользователь вытащил элемент из границы целевого объекта удаления. Параметры hwnd, idObject и idChild функции обратного вызова WinEventProc определяют целевой объект удаления.                                                      </summary>
        EVENT_OBJECT_DRAGLEAVE = 0x8025,
        ///<summary>	Пользователь удалял элемент в целевом объекте удаления. Параметры hwnd, idObject и idChild функции обратного вызова WinEventProc определяют целевой объект удаления.                                                                 </summary>
        EVENT_OBJECT_DRAGDROPPED = 0x8026,
        ///<summary>	Наибольшее значение события объекта.                                                 </summary>
        EVENT_OBJECT_END = 0x80FF,
        ///<summary>	Объект получил фокус клавиатуры. Система отправляет это событие для следующих элементов пользовательского интерфейса: элемента управления представлением списка, строки меню, всплывающего меню, переключения окна, вкладки, элемента управления представлением в виде дерева и объекта окна. Серверные приложения отправляют данное событие для объектов со специальными возможностями.                                                                     </summary>
        EVENT_OBJECT_FOCUS = 0x8005,
        ///<summary>	Параметр hwnd функции обратного вызова WinEventProc определяет окно, которое получает фокус клавиатуры.                                                      </summary>
        ///<summary>	Свойство справки объекта изменилось. Серверные приложения отправляют данное событие для объектов со специальными возможностями.                              </summary>
        EVENT_OBJECT_HELPCHANGE = 0x8010,
        ///<summary>	Объект скрыт. Система отправляет это событие для следующих элементов пользовательского интерфейса: курсора и курсора. Серверные приложения отправляют данное событие для объектов со специальными возможностями.                     </summary>
        EVENT_OBJECT_HIDE = 0x8003,
        ///<summary>	При создании этого события для родительского объекта все дочерние объекты уже скрыты. Серверные приложения не отправляют это событие для дочерних объектов.  </summary>
        ///<summary>	Скрытые объекты включают флаг STATE_SYSTEM_INVISIBLE ; отображаемые объекты не включают этот флаг. Событие EVENT_OBJECT_HIDE также указывает, что установлен флаг STATE_SYSTEM_INVISIBLE . Таким образом, серверы не отправляют событие EVENT_OBJECT_STATECHANGE в этом случае.                              </summary>
        ///<summary>	В окне, в котором размещаются другие доступные объекты, были изменены размещенные объекты. Клиенту может потребоваться запросить окно узла для обнаружения новых размещенных объектов, особенно если клиент отслеживает события из окна. Размещенный объект — это объект из платформы специальных возможностей (MSAA или автоматизации пользовательского интерфейса), который отличается от объекта ведущего приложения. Изменения в размещенных объектах, которые относятся к той же платформе, что и узел, должны передаваться вместе с событиями структурных изменений, такими как EVENT_OBJECT_CREATE для MSAA. Дополнительные сведения см. в комментариях в winuser.h.          </summary>
        EVENT_OBJECT_HOSTEDOBJECTSINVALIDATED = 0x8020,
        ///<summary>	Окно IME стало скрытым.                                                              </summary>
        EVENT_OBJECT_IME_HIDE = 0x8028,
        ///<summary>	Окно IME стало видимым.                                                              </summary>
        EVENT_OBJECT_IME_SHOW = 0x8027,
        ///<summary>	Изменился размер или положение окна IME.                                             </summary>
        EVENT_OBJECT_IME_CHANGE = 0x8029,
        ///<summary>	Объект был вызван; например, пользователь нажал кнопку. Это событие поддерживается общими элементами управления и используется в модели автоматизации пользовательского интерфейса.                                                  </summary>
        EVENT_OBJECT_INVOKED = 0x8013,
        ///<summary>	Для этого события параметры hwnd, ID и idChild функции обратного вызова WinEventProc определяют вызываемый элемент.                                          </summary>
        ///<summary>	Объект, который является частью динамического региона, изменился. Динамический регион — это область приложения, которая часто и /или асинхронно меняется.    </summary>
        EVENT_OBJECT_LIVEREGIONCHANGED = 0x8019,
        ///<summary>	У объекта изменилось расположение, форма или размер. Система отправляет это событие для следующих элементов пользовательского интерфейса: курсора и объектов окна. Серверные приложения отправляют данное событие для объектов со специальными возможностями.                                                </summary>
        EVENT_OBJECT_LOCATIONCHANGE = 0x800B,
        ///<summary>	Это событие создается в ответ на изменение объекта верхнего уровня в иерархии объектов; Он не создается для дочерних элементов, которые могут быть у объекта. Например, если пользователь изменяет размер окна, система отправляет это уведомление для окна, но не для строки меню, заголовка, полосы прокрутки или других объектов, которые также были изменены.                    </summary>
        ///<summary>	При перемещении родительского окна система не отправляет данное событие для каждого неплавающего дочернего окна. Однако если приложение явно изменяет размер дочерних окон в результате изменения размера родительского окна, система отправляет несколько событий для дочерних элементов с измененным размером.                                                                     </summary>
        ///<summary>	Если свойство State объекта имеет значение STATE_SYSTEM_FLOATING, сервер отправляет EVENT_OBJECT_LOCATIONCHANGE при каждом изменении расположения объекта. Если объект не имеет этого состояния, серверы активируют это событие только при перемещении объекта относительно его родительского объекта. Для этого уведомления о событии параметр idChild функции обратного вызова WinEventProc определяет измененный дочерний объект.                         </summary>
        ///<summary>	Свойство Name объекта изменилось. Система отправляет это событие для следующих элементов пользовательского интерфейса: проверка box, cursor, list-view, push button, переключатель, элемент управления строки состояния, элемент управления представлением в виде дерева и объект окна. Серверные приложения отправляют данное событие для объектов со специальными возможностями.   </summary>
        EVENT_OBJECT_NAMECHANGE = 0x800C,
        ///<summary>	У объекта имеется новый родительский объект. Серверные приложения отправляют данное событие для объектов со специальными возможностями.                      </summary>
        EVENT_OBJECT_PARENTCHANGE = 0x800F,
        ///<summary>	Объект контейнера добавил, удалил или переупорядочил дочерние объекты. Система отправляет это событие для следующих элементов пользовательского интерфейса: заголовка, элемента управления представлением списка, элемента управления панелью инструментов и объекта окна. Серверные приложения при необходимости отправляют данное событие для объектов со специальными возможностями.                                                                      </summary>
        EVENT_OBJECT_REORDER = 0x8004,
        ///<summary>	Например, это событие создается объектом представления списка при изменении количества дочерних элементов или порядка элементов. Это событие также отправляется родительским окном при изменении Z-порядка для дочерних окон.        </summary>
        ///<summary>	Выделение в объекте контейнера изменилось. Система отправляет это событие для следующих элементов пользовательского интерфейса: элемента управления list-view, элемента управления tab, элемента управления представлением в виде дерева и объекта окна. Серверные приложения отправляют данное событие для объектов со специальными возможностями.                                  </summary>
        EVENT_OBJECT_SELECTION = 0x8006,
        ///<summary>	Это событие сигнализирует об одном выделении: либо дочерний элемент выбран в контейнере, который ранее не содержал выделенных дочерних элементов, либо выделение изменилось с одного дочернего элемента на другой.                   </summary>
        ///<summary>	Параметры hwnd и idObject функции обратного вызова WinEventProc описывают контейнер; параметр idChild определяет выбранный объект. Если выбранным дочерним элементом является окно, которое также содержит объекты, параметр idChildOBJID_WINDOW.                                                            </summary>
        ///<summary>	Дочерний элемент в объекте контейнера был добавлен к существующему выделенному фрагменту. Система отправляет это событие для следующих элементов пользовательского интерфейса: list box, list-view и tree view. Серверные приложения отправляют данное событие для объектов со специальными возможностями.   </summary>
        EVENT_OBJECT_SELECTIONADD = 0x8007,
        ///<summary>	Параметры hwnd и idObject функции обратного вызова WinEventProc описывают контейнер. Параметр idChild — это дочерний элемент, который добавляется к выделенному фрагменту.                                                           </summary>
        ///<summary>	Из выделения был удален элемент из объекта контейнера. Система отправляет это событие для следующих элементов пользовательского интерфейса: list box, list-view и tree view. Серверные приложения отправляют данное событие для объектов со специальными возможностями.                                      </summary>
        EVENT_OBJECT_SELECTIONREMOVE = 0x8008,
        ///<summary>	Это событие сигнализирует о том, что дочерний элемент удаляется из существующего выделенного фрагмента.                                                      </summary>
        ///<summary>	Параметры hwnd и idObject функции обратного вызова WinEventProc описывают контейнер; Параметр idChild определяет дочернего элемента, который был удален из выделенного фрагмента.                                                    </summary>
        ///<summary>	В объекте контейнера произошли различные изменения выделения. Система отправляет это событие для списков; серверные приложения отправляют его для своих объектов со специальными возможностями.                                      </summary>
        EVENT_OBJECT_SELECTIONWITHIN = 0x8009,
        ///<summary>	Это событие отправляется, когда выбранные элементы в элементе управления существенно изменились. Событие информирует клиента о том, что произошло много изменений выбора, и оно отправляется вместо нескольких событий EVENT_OBJECT_SELECTIONADD или EVENT_OBJECT_SELECTIONREMOVE . Клиент запрашивает выбранные элементы, вызывая метод IAccessible::get_accSelection объекта контейнера и перечисляя выбранные элементы.                                   </summary>
        ///<summary>	Для этого уведомления о событии параметры hwnd и idObject функции обратного вызова WinEventProc описывают контейнер, в котором произошли изменения.          </summary>
        ///<summary>	Отображается скрытый объект. Система отправляет данное событие для следующих элементов пользовательского интерфейса: знак вставки, курсор и объект окна. Серверные приложения отправляют данное событие для объектов со специальными возможностями.                                                          </summary>
        EVENT_OBJECT_SHOW = 0x8002,
        ///<summary>	Клиенты предполагают, что при отправке этого события родительским объектом все дочерние объекты уже отображаются. Поэтому серверные приложения не отправляют это событие для дочерних объектов.                                      </summary>
        ///<summary>	Скрытые объекты включают флаг STATE_SYSTEM_INVISIBLE ; отображаемые объекты не включают этот флаг. Событие EVENT_OBJECT_SHOW также указывает, что флаг STATE_SYSTEM_INVISIBLE снят. Таким образом, серверы не отправляют событие EVENT_OBJECT_STATECHANGE в этом случае.                                     </summary>
        ///<summary>	Состояние объекта изменилось. Система отправляет это событие для следующих элементов пользовательского интерфейса: проверка, поле со списком, заголовок, кнопка нажатия, переключатель, полоса прокрутки, панель инструментов, элемент управления представлением в виде дерева, элемент управления вверх-вниз и объект окна. Серверные приложения отправляют данное событие для объектов со специальными возможностями.                                      </summary>
        EVENT_OBJECT_STATECHANGE = 0x800A,
        ///<summary>	Например, изменение состояния происходит при нажатии или освобождении объекта кнопки, а также при включении или отключении объекта.                          </summary>
        ///<summary>	Для этого уведомления о событии параметр idChild функции обратного вызова WinEventProc определяет дочерний объект, состояние которого изменилось.            </summary>
        ///<summary>	Целевой объект преобразования в композиции IME изменился. Целевой объект преобразования — это подмножество композиции IME, которое активно выбирается в качестве целевого объекта для преобразований, инициированных пользователем.  </summary>
        EVENT_OBJECT_TEXTEDIT_CONVERSIONTARGETCHANGED = 0x8030,
        ///<summary>	Изменено выделение текста объекта. Это событие поддерживается общими элементами управления и используется в модели автоматизации пользовательского интерфейса.                                                                       </summary>
        EVENT_OBJECT_TEXTSELECTIONCHANGED = 0x8014,
        ///<summary>	Параметры hwnd, ID и idChild функции обратного вызова WinEventProc описывают элемент, содержащийся в обновленном фрагменте текста.                           </summary>
        ///<summary>	Отправляется, когда окно не закрыто. Скрытое окно по-прежнему существует, но невидимо для пользователя.                                                      </summary>
        EVENT_OBJECT_UNCLOAKED = 0x8018,
        ///<summary>	Свойство Value объекта изменилось. Система отправляет это событие для элементов пользовательского интерфейса, которые включают полосу прокрутки и следующие элементы управления: правка, заголовок, горячая клавиша, индикатор выполнения, ползунок и вверх-вниз. Серверные приложения отправляют данное событие для объектов со специальными возможностями.                         </summary>
        EVENT_OBJECT_VALUECHANGE = 0x800E,
        ///<summary>	Диапазон значений констант событий, зарезервированных для изготовителей оборудования. Дополнительные сведения см. в разделе Выделение идентификаторов WinEvent.                                                                      </summary>
        EVENT_OEM_DEFINED_END = 0x01FF,
        EVENT_OEM_DEFINED_START = 0x0101,
        ///<summary>	Создано оповещение. Серверные приложения не должны отправлять это событие.           </summary>
        EVENT_SYSTEM_ALERT = 0x0002,
        ///<summary>	Отображается прямоугольник предварительного просмотра.                               </summary>
        EVENT_SYSTEM_ARRANGMENTPREVIEW = 0x8016,
        ///<summary>	Окно потеряло захват мыши. Это событие отправляется системой, а не серверами.        </summary>
        EVENT_SYSTEM_CAPTUREEND = 0x0009,
        ///<summary>	Окно получило захват мыши. Это событие отправляется системой, а не серверами.        </summary>
        EVENT_SYSTEM_CAPTURESTART = 0x0008,
        ///<summary>	Окно завершило контекстно-зависимый режим справки. Это событие не отправляется системой согласованно.                                                        </summary>
        EVENT_SYSTEM_CONTEXTHELPEND = 0x000D,
        ///<summary>	Окно перешло в контекстно-зависимый режим справки. Это событие не отправляется системой согласованно.                                                        </summary>
        EVENT_SYSTEM_CONTEXTHELPSTART = 0x000C,
        ///<summary>	Активный рабочий стол переключен.                                                    </summary>
        EVENT_SYSTEM_DESKTOPSWITCH = 0x0020,
        ///<summary>	Диалоговое окно закрыто. Система отправляет это событие для стандартных диалоговых окон; серверы отправляют его для пользовательских диалоговых окон. Это событие не отправляется системой согласованно.                             </summary>
        EVENT_SYSTEM_DIALOGEND = 0x0011,
        ///<summary>	Откроется диалоговое окно. Система отправляет это событие для стандартных диалоговых окон, созданных с помощью шаблонов ресурсов или функций диалогового окна Win32. Серверы отправляют это событие для пользовательских диалоговых окон, которые функционируют как диалоговые окна, но не создаются стандартным способом.                                                           </summary>
        EVENT_SYSTEM_DIALOGSTART = 0x0010,
        ///<summary>	Это событие не отправляется системой согласованно.                                   </summary>
        ///<summary>	Приложение выходит из режима перетаскивания. Приложения, поддерживающие операции перетаскивания, должны отправлять это событие; система не отправляет это событие.                                                                   </summary>
        EVENT_SYSTEM_DRAGDROPEND = 0x000F,
        ///<summary>	Приложение входит в режим перетаскивания. Приложения, поддерживающие операции перетаскивания, должны отправлять это событие, так как система не отправляет его.                                                                      </summary>
        EVENT_SYSTEM_DRAGDROPSTART = 0x000E,
        ///<summary>	Наибольшее значение системного события.                                              </summary>
        EVENT_SYSTEM_END = 0x00FF,
        ///<summary>	Окно переднего плана изменилось. Система отправляет это событие, даже если окно переднего плана изменилось на другое окно в том же потоке. Серверные приложения никогда не отправляют данное событие.                                </summary>
        EVENT_SYSTEM_FOREGROUND = 0x0003,
        ///<summary>	Для этого события параметр hwnd функции обратного вызова WinEventProc — это дескриптор окна на переднем плане, параметр idObject — OBJID_WINDOW, а параметр idChild — CHILDID_SELF.                                                  </summary>
        ///<summary>	Всплывающее меню закрыто. Система отправляет это событие для стандартных меню; серверы отправляют его для пользовательских меню.                             </summary>
        EVENT_SYSTEM_MENUPOPUPEND = 0x0007,
        ///<summary>	При закрытии всплывающего меню клиент получает это сообщение, а затем событие EVENT_SYSTEM_MENUEND .                                                         </summary>
        ///<summary>	Это событие не отправляется системой согласованно.                                   </summary>
        ///<summary>	Отображается всплывающее меню. Система отправляет это событие для стандартных меню, которые определяются HMENU и создаются с помощью ресурсов шаблона меню или функций меню Win32. Серверы отправляют это событие для пользовательских меню, которые являются элементами пользовательского интерфейса, которые функционируют как меню, но не создаются стандартным способом. Это событие не отправляется системой согласованно.                              </summary>
        EVENT_SYSTEM_MENUPOPUPSTART = 0x0006,
        ///<summary>	Меню в строке меню закрыто. Система отправляет это событие для стандартных меню; серверы отправляют его для пользовательских меню.                           </summary>
        EVENT_SYSTEM_MENUEND = 0x0005,
        ///<summary>	Для этого события параметры функции обратного вызова WinEventProchwnd, idObject и idChild ссылаются на элемент управления, содержащий строку меню, или элемент управления, активизующий контекстное меню. Параметр hwnd — это дескриптор окна, связанного с событием . Параметр idObjectOBJID_MENU илиOBJID_SYSMENU для меню или OBJID_WINDOW для всплывающего меню. Параметр idChildимеет значение CHILDID_SELF.                                            </summary>
        ///<summary>	Выбран пункт меню в строке меню. Система отправляет это событие для стандартных меню, которые определяются HMENU, созданные с помощью ресурсов шаблона меню или элементов API меню Win32. Серверы отправляют это событие для пользовательских меню, которые являются элементами пользовательского интерфейса, которые функционируют как меню, но не создаются стандартным способом.  </summary>
        EVENT_SYSTEM_MENUSTART = 0x0004,
        ///<summary>	Для этого события параметры функции обратного вызова WinEventProchwnd, idObject и idChild ссылаются на элемент управления, содержащий строку меню, или элемент управления, активизующий контекстное меню. Параметр hwnd — это дескриптор окна, связанного с событием . Параметр idObjectOBJID_MENU илиOBJID_SYSMENU для меню или OBJID_WINDOW для всплывающего меню. Параметр idChildимеет значение CHILDID_SELF.                                            </summary>
        ///<summary>	Система активирует несколько EVENT_SYSTEM_MENUSTART событий, которые не всегда соответствуют событию EVENT_SYSTEM_MENUEND .                                  </summary>
        ///<summary>	Объект окна будет восстановлен. Это событие отправляется системой, а не серверами.   </summary>
        EVENT_SYSTEM_MINIMIZEEND = 0x0017,
        ///<summary>	Объект окна будет свернут. Это событие отправляется системой, а не серверами.        </summary>
        EVENT_SYSTEM_MINIMIZESTART = 0x0016,
        ///<summary>	Перемещение или изменение размера окна завершено. Это событие отправляется системой, а не серверами.                                                         </summary>
        EVENT_SYSTEM_MOVESIZEEND = 0x000B,
        ///<summary>	Происходит изменение размеров окна или его перемещение. Это событие отправляется системой, а не серверами.                                                   </summary>
        EVENT_SYSTEM_MOVESIZESTART = 0x000A,
        ///<summary>	На полосе прокрутки прокрутка закончилась. Это событие отправляется системой для стандартных элементов управления полосой прокрутки и для полос прокрутки, прикрепленных к окну. Серверы отправляют это событие для пользовательских полос прокрутки, которые представляют собой элементы пользовательского интерфейса, которые функционируют как полосы прокрутки, но не создаются стандартным способом.                                                    </summary>
        EVENT_SYSTEM_SCROLLINGEND = 0x0013,
        ///<summary>	Параметр idObject , отправляемый в функцию обратного вызова WinEventProc , OBJID_HSCROLL для горизонтальных полос прокрутки и OBJID_VSCROLL для вертикальных полос прокрутки.                                                        </summary>
        ///<summary>	На полосе прокрутки началась прокрутка. Система отправляет это событие для стандартных элементов управления полосой прокрутки и полос прокрутки, прикрепленных к окну. Серверы отправляют это событие для пользовательских полос прокрутки, которые представляют собой элементы пользовательского интерфейса, которые функционируют как полосы прокрутки, но не создаются стандартным способом.                                                              </summary>
        EVENT_SYSTEM_SCROLLINGSTART = 0x0012,
        ///<summary>	Параметр idObject , отправляемый в функцию обратного вызова WinEventProc , OBJID_HSCROLL для горизонтальных полос прокрутки и OBJID_VSCROLL для вертикальных полос прокрутки.                                                        </summary>
        ///<summary>	Звук был воспроизведен. Система отправляет это событие при воспроизведении системного звука, например для меню, даже если звук не слышен (например, из-за отсутствия звукового файла или звукового карта). Серверы отправляют это событие всякий раз, когда пользовательский элемент пользовательского интерфейса создает звук.                                                      </summary>
        EVENT_SYSTEM_SOUND = 0x0001,
        ///<summary>	Для этого события функция обратного вызова WinEventProc получает значение OBJID_SOUND в качестве параметра idObject .                                        </summary>
        ///<summary>	Пользователь выпустил сочетание клавиш ALT+TAB. Это событие отправляется системой, а не серверами. Параметр hwnd функции обратного вызова WinEventProc определяет окно, в которое переключился пользователь.                         </summary>
        EVENT_SYSTEM_SWITCHEND = 0x0015,
        ///<summary>	Если при нажатии клавиш ALT+TAB выполняется только одно приложение, система отправляет это событие без соответствующего события EVENT_SYSTEM_SWITCHSTART .   </summary>
        ///<summary>	Пользователь нажимает клавиши ALT+TAB, что активирует окно переключения. Это событие отправляется системой, а не серверами. Параметр hwnd функции обратного вызова WinEventProc определяет окно, в которое переключается пользователь.                                                                       </summary>
        EVENT_SYSTEM_SWITCHSTART = 0x0014,
        ///<summary>	Если при нажатии клавиш ALT+TAB выполняется только одно приложение, система отправляет событие EVENT_SYSTEM_SWITCHEND без соответствующего события EVENT_SYSTEM_SWITCHSTART .                                                        </summary>
        EVENT_UIA_EVENTID_START = 0x4EFF,
        ///<summary>	Диапазон значений констант событий, зарезервированных для идентификаторов событий модели автоматизации пользовательского интерфейса. Дополнительные сведения см. в разделе Выделение идентификаторов WinEvent.                       </summary>
        EVENT_UIA_EVENTID_END = 0x4E00,
        EVENT_UIA_PROPID_START = 0x7500,
        ///<summary>	Диапазон значений констант событий, зарезервированных для идентификаторов измененных свойств в модели автоматизации пользовательского интерфейса. Дополнительные сведения см. в разделе Выделение идентификаторов WinEvent.          </summary>
        EVENT_UIA_PROPID_END = 0x75FF,
    }
}